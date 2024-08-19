using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private AudioClip finishGameMusic;

    public static GameManager Instance { get; private set; }

    public event EventHandler OnGameStateChanged;
    public event EventHandler<OnLadderAddedEventArgs> OnLadderAdded;
    public class OnLadderAddedEventArgs : EventArgs {
        public Vector2 ladderPosition;
    }
    public event EventHandler OnLadderLost;
    public event EventHandler OnLifeLost;
    public event EventHandler OnGameOver;
    public event EventHandler OnCompleteGame;

    private enum State {
        WaitingToStart,
        GamePlaying,
        GameOver,
        ReachedTheMoon,
    }

    private State state;
    private GameObject currentFallingLadder;
    private float timeToReachTheMoon = 0;
    private int numberOfLives = 5;
    private int currentNumberOfLadders;
    private List<GameObject> currentBuiltLadder = new List<GameObject>();

    private void Awake() {
        Instance = this;
        state = State.WaitingToStart;
        currentFallingLadder = null;
    }

    private void Start() {
        LayerManager.Instance.OnLayerChanged += LayerManager_OnLayerChanged;
        GameInputManager.Instance.OnAnyKeyAction += GameInputManager_OnAnyKeyAction;

        var baseLadder = FindObjectOfType<Ladder>().gameObject;
        Destroy(baseLadder.GetComponent<Ladder>());
        currentBuiltLadder.Add(baseLadder);
    }

    private void GameInputManager_OnAnyKeyAction(object sender, EventArgs e) {
        if (state == State.WaitingToStart) {
            ChangeState(State.GamePlaying);
        }
    }

    private void LayerManager_OnLayerChanged(object sender, EventArgs e) {
        if (LayerManager.Instance.GetCurrentLayer() == LayerManager.Layer.Moon) {
            ChangeState(State.ReachedTheMoon);
            MusicManager.Instance.ChangeMusic(finishGameMusic);
            UpdateHighScoreIfNecessary();
            OnCompleteGame?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Update() {
        switch (state) {
            case State.WaitingToStart:
                break;
            case State.GamePlaying:
                timeToReachTheMoon += Time.deltaTime;
                bool isCurrentLadderNull = currentFallingLadder == null;
                bool didNotReachTheMoon = currentNumberOfLadders < LayerManager.Instance.GetMaxHeightForLayer(LayerManager.Layer.Space);
                bool shouldSpawnNewLadder = isCurrentLadderNull && didNotReachTheMoon;
                if (shouldSpawnNewLadder) {
                    currentFallingLadder = LadderSpawner.Instance.SpawnNewLadder();
                }
                break;
            case State.GameOver:
                break;
            case State.ReachedTheMoon:
                break;
        }
    }

    private void ChangeState(State newState) {
        state = newState;
        OnGameStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void UpdateHighScoreIfNecessary() {
        var currentScore = new StackingScore(Player.Instance.GetDistanceFromCenter(), timeToReachTheMoon, numberOfLives);
        HighScore.SaveHighScoreIfNecessary(currentScore);
    }

    public bool IsGamePlaying() {
        return state == State.GamePlaying;
    }

    public int GetCurrentHeight() {
        return currentBuiltLadder.Count;
    }

    public void AddLadderPiece(GameObject ladder) {
        currentFallingLadder = null;
        currentNumberOfLadders++;

        // To avoid all collision to previously added ladders, remove all rigidBodies except last one
        if (currentBuiltLadder.Count >= 1) {
            GameObject previousToLastLadder = currentBuiltLadder[currentBuiltLadder.Count - 1];
            if (previousToLastLadder != null) {
                Destroy(previousToLastLadder.GetComponent<Rigidbody2D>());
                Destroy(previousToLastLadder.GetComponent<Collider2D>());
            }
        }
        currentBuiltLadder.Add(ladder);

        OnLadderAdded?.Invoke(this, new OnLadderAddedEventArgs {
            ladderPosition = ladder.transform.position,
        });
    }

    public void LoseLadder() {
        numberOfLives--;
        currentFallingLadder = null;
        OnLadderLost?.Invoke(this, EventArgs.Empty);
        OnLifeLost?.Invoke(this, EventArgs.Empty);

        if (numberOfLives <= 0) {
            ChangeState(State.GameOver);
            OnGameOver?.Invoke(this, EventArgs.Empty);
        }
    }

    public int GetNumberOfLives() { return numberOfLives; }
}

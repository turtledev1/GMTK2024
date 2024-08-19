using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour {

    [SerializeField] private AudioClip finishGameMusic;

    public static GameManager Instance { get; private set; }

    public event EventHandler OnGameStateChanged;
    public event EventHandler OnTogglePause;
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
    private int numberOfLives = 5;
    private int currentNumberOfLadders;
    private List<GameObject> currentBuiltLadder = new List<GameObject>();

    private float TEMPORARY_TIMER_TIME = 2f;
    private float TEMPORARY_TIMER = 0f;

    private void Awake() {
        Instance = this;
        state = State.WaitingToStart;
        currentFallingLadder = null;
    }

    private void Start() {
        LayerManager.Instance.OnLayerChanged += LayerManager_OnLayerChanged;

        var baseLadder = FindObjectOfType<Ladder>().gameObject;
        Destroy(baseLadder.GetComponent<Ladder>());
        currentBuiltLadder.Add(baseLadder);
    }

    private void LayerManager_OnLayerChanged(object sender, EventArgs e) {
        if (LayerManager.Instance.GetCurrentLayer() == LayerManager.Layer.Moon) {
            ChangeState(State.ReachedTheMoon);
            MusicManager.Instance.ChangeMusic(finishGameMusic);
            OnCompleteGame?.Invoke(this, EventArgs.Empty);
        }
    }

    private void Update() {
        switch (state) {
            case State.WaitingToStart:
                TEMPORARY_TIMER += Time.deltaTime;
                if (TEMPORARY_TIMER > TEMPORARY_TIMER_TIME) {
                    ChangeState(State.GamePlaying);
                }
                break;
            case State.GamePlaying:
                if (currentFallingLadder == null) {
                    currentFallingLadder = LadderSpawner.Instance.SpawnNewLadder();
                }
                break;
            case State.GameOver:
                Debug.Log("GameOver");
                break;
            case State.ReachedTheMoon:
                break;
        }
    }

    private void ChangeState(State newState) {
        state = newState;
        OnGameStateChanged?.Invoke(this, EventArgs.Empty);
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
        Debug.Log("OnLadderAdded: " + currentNumberOfLadders);
    }

    public void LoseLadder() {
        numberOfLives--;
        currentFallingLadder = null;
        OnLadderLost?.Invoke(this, EventArgs.Empty);
        OnLifeLost?.Invoke(this, EventArgs.Empty);

        Debug.Log("Life lost: " + numberOfLives);

        if (numberOfLives <= 0) {
            ChangeState(State.GameOver);
            OnGameOver?.Invoke(this, EventArgs.Empty);
        }
    }

    public int GetNumberOfLives() { return numberOfLives; }
}

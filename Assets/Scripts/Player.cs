using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private float moveSpeed = 1f;

    public static Player Instance { get; private set; }

    private bool shouldMove = false;
    private Vector2 positionToGo;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        GameManager.Instance.OnLadderAdded += GameManager_OnLadderAdded;
    }

    private void GameManager_OnLadderAdded(object sender, GameManager.OnLadderAddedEventArgs e) {
        positionToGo = e.ladderPosition;
        shouldMove = true;
    }

    private void Update() {
        if (shouldMove) {
            transform.position = Vector2.MoveTowards(transform.position, positionToGo, moveSpeed * Time.deltaTime);
        }
    }

    public float GetCurrentHeight() {
        return transform.position.y;
    }
}

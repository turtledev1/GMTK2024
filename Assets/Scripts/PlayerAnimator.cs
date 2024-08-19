using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    private const string SAD_TRIGGER = "Sad";
    private const string IS_MOVING = "IsMoving";
    private const string IS_DEAD = "IsDead";
    private const string IS_GAME_FINISHED = "IsGameFinished";

    private Animator playerAnimator;

    private void Awake() {
        playerAnimator = GetComponent<Animator>();
    }

    void Start() {
        GameManager.Instance.OnLadderLost += GameManager_OnLadderLost;
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;
        GameManager.Instance.OnCompleteGame += GameManager_OnCompleteGame;
    }

    private void GameManager_OnCompleteGame(object sender, System.EventArgs e) {
        playerAnimator.SetBool(IS_GAME_FINISHED, true);
    }

    private void GameManager_OnGameOver(object sender, System.EventArgs e) {
        playerAnimator.SetBool(IS_DEAD, true);
    }

    private void Update() {
        playerAnimator.SetBool(IS_MOVING, Player.Instance.IsMoving());
    }

    private void GameManager_OnLadderLost(object sender, System.EventArgs e) {
        playerAnimator.SetTrigger(SAD_TRIGGER);
    }
}

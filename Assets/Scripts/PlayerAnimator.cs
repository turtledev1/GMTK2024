using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour {

    private const string SAD_TRIGGER = "Sad";

    private Animator playerAnimator;

    private void Awake() {
        playerAnimator = GetComponent<Animator>();
    }

    void Start() {
        GameManager.Instance.OnLadderLost += GameManager_OnLadderLost;
    }

    private void GameManager_OnLadderLost(object sender, System.EventArgs e) {
        playerAnimator.SetTrigger(SAD_TRIGGER);
    }
}

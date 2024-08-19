using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReachedTheMoonUI : MonoBehaviour {

    [SerializeField] private Transform epilogueText;

    void Start() {
        GameManager.Instance.OnCompleteGame += GameManager_OnCompleteGame;

        Hide();
    }

    private void GameManager_OnCompleteGame(object sender, System.EventArgs e) {
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {

    [SerializeField] private Button tryAgainButton;

    private void Awake() {
        tryAgainButton.onClick.AddListener(() => {
            SceneManager.LoadScene(SceneLoader.Scene.GameScene.ToString());
        });
    }

    private void Start() {
        GameManager.Instance.OnGameOver += GameManager_OnGameOver;

        Hide();
    }

    private void GameManager_OnGameOver(object sender, System.EventArgs e) {
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}

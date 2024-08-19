using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReachedTheMoonUI : MonoBehaviour {

    [SerializeField] private TextTypewriter finalText;
    [SerializeField] private Button mainMenuButton;

    private void Awake() {
        mainMenuButton.onClick.AddListener(() => {
            SceneLoader.Load(SceneLoader.Scene.MainMenu);
        });
    }

    void Start() {
        GameManager.Instance.OnCompleteGame += GameManager_OnCompleteGame;

        mainMenuButton.gameObject.SetActive(false);
        Hide();
    }

    private void GameManager_OnCompleteGame(object sender, System.EventArgs e) {
        Show();
    }

    private void Show() {
        gameObject.SetActive(true);
        finalText.StartShowing(() => ShowMainMenuButton());
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void ShowMainMenuButton() {
        mainMenuButton.gameObject.SetActive(true);
    }
}

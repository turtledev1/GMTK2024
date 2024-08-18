using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivesUI : MonoBehaviour {

    [SerializeField] private Transform container;
    [SerializeField] private Transform lifeTemplate;

    private void Awake() {
        lifeTemplate.gameObject.SetActive(false);
    }

    void Start() {
        UpdateVisuals();

        GameManager.Instance.OnLifeLost += GameManager_OnLifeLost;
    }

    private void GameManager_OnLifeLost(object sender, System.EventArgs e) {
        UpdateVisuals();
    }

    private void UpdateVisuals() {
        int currentNumberOfLives = GameManager.Instance.GetNumberOfLives();

        // This one is not active, just act as a template
        foreach (Transform child in container) {
            if (child == lifeTemplate) {
                continue;
            }
            Destroy(child.gameObject);
        }

        for (int i = 0; i < currentNumberOfLives; i++) {
            var icon = Instantiate(lifeTemplate, container);
            icon.gameObject.SetActive(true);
        }
    }
}

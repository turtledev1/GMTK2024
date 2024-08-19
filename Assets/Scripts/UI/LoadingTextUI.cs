using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadingTextUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI loadingText;

    private enum ThreeDotsState {
        Zero,
        One,
        Two,
        Three,
    }

    private ThreeDotsState state;
    private float threeDotsTimerMax = 0.8f;
    private float threeDotsTimer;

    private void Awake() {
        loadingText.text = "LOADING";
        state = ThreeDotsState.Zero;
    }

    private void Update() {
        threeDotsTimer += Time.deltaTime;
        if (threeDotsTimer >= threeDotsTimerMax) {
            threeDotsTimer = 0f;
            switch (state) {
                case ThreeDotsState.Zero:
                    state = ThreeDotsState.One;
                    loadingText.text = "LOADING.";
                    break;
                case ThreeDotsState.One:
                    state = ThreeDotsState.Two;
                    loadingText.text = "LOADING..";
                    break;
                case ThreeDotsState.Two:
                    state = ThreeDotsState.Three;
                    loadingText.text = "LOADING...";
                    break;
                case ThreeDotsState.Three:
                    state = ThreeDotsState.Zero;
                    loadingText.text = "LOADING";
                    break;
            }
        }
    }
}

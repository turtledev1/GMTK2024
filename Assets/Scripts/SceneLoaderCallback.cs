using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderCallback : MonoBehaviour {

    private float minTimeToWait = 3f;
    private float loadingTime;

    private void Update() {
        loadingTime += Time.deltaTime;
        if (loadingTime >= minTimeToWait) {
            SceneLoader.SceneLoaderCallback();
        }
    }
}

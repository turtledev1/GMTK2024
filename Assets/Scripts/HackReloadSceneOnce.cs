using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HackReloadSceneOnce : MonoBehaviour
{
    // This variable will be used to check if the scene has been reloaded
    private static bool hasReloaded = false;

    void Start() {
        // Check if the scene has already been reloaded
        if (!hasReloaded) {
            // Set the flag to true so it won't reload again
            hasReloaded = true;

            // Reload the current scene
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}

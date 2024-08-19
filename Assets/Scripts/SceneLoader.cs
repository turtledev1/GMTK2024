using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader {

    public enum Scene {
        MainMenu,
        GameScene,
        LoadingScene
    }

    private static Scene targetScene;

    public static void Load(Scene targetScene) {
        SceneLoader.targetScene = targetScene;

        SceneManager.LoadScene(Scene.LoadingScene.ToString());
    }

    public static void SceneLoaderCallback() {
        SceneManager.LoadScene(targetScene.ToString());
    }
}

﻿using UnityEngine.SceneManagement;

public static class Loader
{
    public enum Scene
    {
        LoadingScene,
        MenuScene,
        GameplayScene,
    }

    private static Scene targetScene;

    public static void Load(Scene targetScene, System.Action afterLoadScene = null)
    {
        Loader.targetScene = targetScene;
        SceneManager.LoadScene(Loader.targetScene.ToString());
    }
}
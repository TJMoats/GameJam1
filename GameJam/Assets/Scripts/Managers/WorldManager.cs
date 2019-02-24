using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class WorldManager : SerializedMonoBehaviour
{
    #region Singleton
    private static WorldManager instance;
    public static WorldManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<WorldManager>();
            }
            return instance;
        }
    }
    #endregion  

    private void Awake()
    {
        instance = this;
        NPS.SceneHelper.primarySceneChanged += OnPrimarySceneChanged;
    }

    private void OnPrimarySceneChanged(NPS.SceneLoader _sceneController)
    {
        List<string> transitionalScenesToLoad = GetTransitionalScenesToLoad();
        List<string> nonTransitionalScenesToLoad = GetNonTransitionalScenesToLoad();
        List<string> scenesToUnload = GetScenesToUnload();

        scenesToUnload.ForEach((string sceneName) =>
        {
            StartCoroutine(NPS.SceneHelper.UnloadScene(sceneName));
        });
        transitionalScenesToLoad.ForEach((string sceneName) =>
        {
            StartCoroutine(NPS.SceneHelper.PreloadScene(sceneName));
            NPS.SceneHelper.GetSceneCache(sceneName).Active = false;
        });
        nonTransitionalScenesToLoad.ForEach((string sceneName) =>
        {
            StartCoroutine(NPS.SceneHelper.PreloadScene(sceneName));
            NPS.SceneHelper.GetSceneCache(sceneName).Active = true;
        });
    }

    private List<string> GetTransitionalScenesToLoad()
    {
        return (NPS.SceneHelper.PrimarySceneController as SceneController).GetTransitionalScenes();
    }

    private List<string> GetNonTransitionalScenesToLoad()
    {
        return (NPS.SceneHelper.PrimarySceneController as SceneController).GetAdjacentScenes();
    }

    private List<string> GetScenesToUnload()
    {
        List<string> availableScenes = NPS.SceneHelper.AvailableScenes;
        availableScenes.Remove(NPS.SceneHelper.PrimarySceneName);
        foreach (string sceneName in GetTransitionalScenesToLoad())
        {
            if (availableScenes.Contains(sceneName))
            {
                availableScenes.Remove(sceneName);
            }
        }
        foreach (string sceneName in GetNonTransitionalScenesToLoad())
        {
            if (availableScenes.Contains(sceneName))
            {
                availableScenes.Remove(sceneName);
            }
        }
        return availableScenes;
    }
}

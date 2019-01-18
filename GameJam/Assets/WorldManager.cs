using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    private AsyncOperation loadingScene;

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

    [SerializeField, ReadOnly]
    private string currentScene;
    public string CurrentScene
    {
        get => currentScene;
        private set => currentScene = value;
    }

    public static void ChangeScene(string _newScene, string _transitionId)
    {
        if (Instance.CurrentScene == _newScene)
        {
            Debug.Log("Tried to load into the current scene!");
            return;
        }
        
        Instance.SwapActiveScene(Instance.CurrentScene, _newScene);
    }

    private void SwapActiveScene(string _currentScene, string _newScene)
    {
        Debug.Log($"Swapping from {_currentScene} to {_newScene}");
        StartCoroutine(LoadScene(_newScene));
    }

    private IEnumerator LoadScene(string _newScene)
    {
        AsyncOperation scene = SceneManager.LoadSceneAsync(_newScene, LoadSceneMode.Additive);
        scene.allowSceneActivation = false;
        loadingScene = scene;

        while (scene.progress < 0.9f)
        {
            yield return null;
        }
        OnFinishedLoadingScene(_newScene);
    }

    void OnFinishedLoadingScene(string _newScene)
    {
        loadingScene.allowSceneActivation = true;

        Scene sceneToLoad = SceneManager.GetSceneByName(_newScene);
        if (sceneToLoad.IsValid())
        {
            SceneManager.MoveGameObjectToScene(MasterManager.Instance.PersistantGameContainer, sceneToLoad);
            SceneManager.SetActiveScene(sceneToLoad);
            AsyncOperation scene = SceneManager.UnloadSceneAsync(CurrentScene);
            CurrentScene = _newScene;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        CurrentScene = SceneManager.GetActiveScene().name;
    }
}

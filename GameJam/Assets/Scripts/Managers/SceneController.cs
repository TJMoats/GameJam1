using NPS;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PolygonCollider2D))]
public class SceneController : NPS.SceneController
{
    [SerializeField]
    private PolygonCollider2D cameraBounds;
    public PolygonCollider2D CameraBounds
    {
        get
        {
            if (cameraBounds == null)
            {
                GetComponent<PolygonCollider2D>();
            }
            return cameraBounds;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        if (MasterManager.Instance == null)
        {
            throw new Exception("The master manager doesn't exist!");
        }
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        SceneManager.activeSceneChanged += SceneManager_activeSceneChanged;
    }

    private void SceneManager_activeSceneChanged(Scene _scene1, Scene _scene2)
    {
        Debug.Log($"{_scene1.name} {_scene2.name}", gameObject);
    }

    private void SceneManager_sceneLoaded(Scene _scene, LoadSceneMode _loadSceneMode)
    {
        Debug.Log($"Scene Loaded: {_scene.name}.", gameObject);
    }

    private List<string> GetAdjacentScenes()
    {
        List<string> sceneList = new List<string>();
        SceneTransition[] sceneTransitions = FindObjectsOfType<SceneTransition>();
        foreach (SceneTransition s in sceneTransitions)
        {
            sceneList.Add(s.TargetSceneName);
        }
        return sceneList;
    }
}

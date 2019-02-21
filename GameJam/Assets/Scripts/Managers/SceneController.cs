using NPS;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PolygonCollider2D))]
public class SceneController : NPS.SceneController
{
    [SerializeField]
    private PolygonCollider2D sceneBounds;
    public PolygonCollider2D SceneBounds
    {
        get
        {
            if (sceneBounds == null)
            {
                sceneBounds = GetComponent<PolygonCollider2D>();
            }
            return sceneBounds;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        if (MasterManager.Instance == null)
        {
            throw new Exception("The master manager doesn't exist!");
        }
        SceneHelper.RegisterScene(SceneName, this);
        if (SceneManager.GetActiveScene().name == SceneName)
        {
            List<string> adjacentScenes = GetAdjacentScenes();
            adjacentScenes.ForEach((string sceneName) =>
            {
                StartCoroutine(SceneHelper.PreloadScene(sceneName));
                SceneHelper.SetActive(sceneName, true);
            });

            List<string> transitionalScenes = GetTransitionalScenes();
            transitionalScenes.ForEach((string sceneName) =>
            {
                StartCoroutine(SceneHelper.PreloadScene(sceneName));
                SceneHelper.SetActive(sceneName, false);
            });
        }
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        PlayerController player = _collision.gameObject?.GetComponent<PlayerController>();
        if (player != null)
        {
            SceneHelper.PrimarySceneName = SceneName;
        }
    }
    
    private List<string> GetAdjacentScenes()
    {
        List<string> sceneList = new List<string>();
        SceneTransition[] sceneTransitions = FindObjectsOfType<SceneScrollTransition>();
        foreach (SceneTransition s in sceneTransitions)
        {
            sceneList.Add(s.TargetSceneName);
        }
        return sceneList;
    }

    private List<string> GetTransitionalScenes()
    {
        List<string> sceneList = new List<string>();
        FadeOutTransition[] sceneTransitions = FindObjectsOfType<FadeOutTransition>();
        foreach (FadeOutTransition s in sceneTransitions)
        {
            Debug.Log(s.TargetSceneName);
            sceneList.Add(s.TargetSceneName);
        }
        return sceneList;
    }

    private void SetPrimary()
    {

    }
}

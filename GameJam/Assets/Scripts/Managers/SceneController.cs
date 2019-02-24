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
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        PlayerController player = _collision.gameObject?.GetComponent<PlayerController>();
        if (player != null)
        {
            SceneHelper.PrimarySceneName = SceneName;
        }
    }

    public List<string> GetAdjacentScenes()
    {
        List<string> sceneList = new List<string>();
        SceneTransition[] sceneTransitions = GetComponentsInChildren<SceneScrollTransition>();
        foreach (SceneTransition s in sceneTransitions)
        {
            sceneList.Add(s.TargetSceneName);
        }
        return sceneList;
    }

    public List<string> GetTransitionalScenes()
    {
        List<string> sceneList = new List<string>();
        FadeOutTransition[] sceneTransitions = GetComponentsInChildren<FadeOutTransition>();
        foreach (FadeOutTransition s in sceneTransitions)
        {
            sceneList.Add(s.TargetSceneName);
        }
        return sceneList;
    }
}

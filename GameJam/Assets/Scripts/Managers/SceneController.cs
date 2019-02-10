using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PolygonCollider2D))]
public class SceneController : SerializedMonoBehaviour
{
    [SerializeField]
    private SceneData sceneData;
    public SceneData SceneData
    {
        get
        {
            if (sceneData == null)
            {
                throw new System.Exception("This scene is missing it's data!");
            }
            return sceneData;
        }
    }

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

    private void OnValidate()
    {
        SceneData.SceneName = SceneManager.GetActiveScene().name;
        SceneTransition[] availableTransitions = FindObjectsOfType<SceneTransition>();
    }

    private void Awake()
    {
        if (MasterManager.Instance == null)
        {
            throw new Exception("The master manager doesn't exist!");
        }
    }
}

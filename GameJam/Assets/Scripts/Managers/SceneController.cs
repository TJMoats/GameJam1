using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PolygonCollider2D))]
public class SceneController : SerializedMonoBehaviour
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

    private void OnValidate()
    {

    }

    private void Awake()
    {
        if (MasterManager.Instance == null)
        {
            throw new Exception("The master manager doesn't exist!");
        }
    }
}

using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PolygonCollider2D))]
public class SceneManager : SerializedMonoBehaviour
{
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
}

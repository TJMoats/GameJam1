using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : SerializedMonoBehaviour
{
    private Collider2D collider2D;
    protected Collider2D Collider2D
    {
        get
        {
            if (collider2D == null)
            {
                collider2D = GetComponent<Collider2D>();
            }
            return collider2D;
        }
    }

    private void Awake()
    {
        Collider2D.isTrigger = true;
    }
}

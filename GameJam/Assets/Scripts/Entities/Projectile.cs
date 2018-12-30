using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : Entity
{
    [SerializeField, Range(1, 50)]
    private float speed = 10f;

    [SerializeField]
    private Vector2 target;
    public Vector2 Target
    {
        get => target;
        set => target = value;
    }

    [SerializeField]
    private GameObject targetObject;
    public GameObject TargetObject
    {
        get => TargetObject;
        set => targetObject = value;
    }

    public void SetTarget()
    {

    }

    private void Start()
    {
        
    }
}

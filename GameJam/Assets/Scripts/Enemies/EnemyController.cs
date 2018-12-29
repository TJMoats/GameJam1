using Pathfinding;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AIPath), typeof(Seeker))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Transform target;

    private AIPath aiPath;
    protected AIPath AIPath
    {

        get
        {
            if (aiPath == null)
            {
                aiPath = GetComponent<AIPath>();
            }
            return aiPath;
        }
    }

    private void Update()
    {
    }

    [Button]
    public void UpdateTarget()
    {
        AIPath.destination = target.position;

    }
}

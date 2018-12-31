using BehaviorDesigner.Runtime;
using Pathfinding;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BehaviorTree))]
[RequireComponent(typeof(Seeker), typeof(AIPath))]
public class NPCController : Entity
{
    [SerializeField]
    private NPCType type;
    public NPCType Type
    {
        get => type;
        set => type = value;
    }

    [SerializeField]
    private float maxMovementSpeed = 3f;
    [SerializeField]
    private float movementAcceleration = 1f;

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

    private CombatComponent combatComponent;
    protected CombatComponent CombatComponent
    {
        get
        {
            if (combatComponent == null)
            {
                combatComponent = gameObject.GetOrAddComponent<CombatComponent>();
            }
            return combatComponent;
        }
    }

    private BehaviorTree behaviorTree;
    protected BehaviorTree BehaviorTree
    {
        get
        {
            if (behaviorTree == null)
            {
                behaviorTree = GetComponent<BehaviorTree>();
            }
            return behaviorTree;
        }
    }

    private Transform handAnchor;
    protected Transform HandAnchor
    {
        get
        {
            if (handAnchor == null)
            {
                handAnchor = transform.Find("HandAnchor");
                if (handAnchor == null)
                {
                    GameObject go = new GameObject("HandAnchor");
                    go.transform.SetParent(transform);
                    go.transform.localPosition = Vector3.zero;
                    handAnchor = go.transform;
                }
            }
            return handAnchor;
        }
    }

    private Animator spriteAnimator;
    protected Animator SpriteAnimator
    {
        get
        {
            if (spriteAnimator == null)
            {
                spriteAnimator = transform.Find("Sprite")?.gameObject?.GetComponent<Animator>();
            }
            return spriteAnimator;
        }
    }

    [SerializeField]
    private Direction facing = Direction.down;
    private void UpdateFacing()
    {
        Vector3 direction = transform.position - aiPath.steeringTarget;
        Debug.Log(direction);
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            // Left or right
            facing = direction.x > 0 ? Direction.left : Direction.right;
        }
        else
        {
            // Up or down
            facing = direction.y > 0 ? Direction.down : Direction.up;
        }
        
        Animator.SetFloat("Direction", (int)facing);
        HandAnchor.transform.eulerAngles = new Vector3(0, 0, (int)facing * -90);
    }

    private void Update()
    {
        UpdateFacing();
    }

    private void Start()
    {
        InitializePathfinding();
        InitializeBehaviorTree();
    }

    private void InitializePathfinding()
    {
        AIPath.gravity = new Vector3(0, 0, 0);
        AIPath.orientation = OrientationMode.YAxisForward;
        AIPath.maxSpeed = maxMovementSpeed;
        AIPath.maxAcceleration = movementAcceleration;
        // If we have a sprite animator, we have 4 direction turning
        AIPath.enableRotation = (SpriteAnimator == null);
    }

    private void InitializeBehaviorTree()
    {
        if (BehaviorTree != null)
        {
            SharedFloat movementSpeed = maxMovementSpeed;

            BehaviorTree.SetVariable("MovementSpeed", movementSpeed);
        }
    }

    private void OnValidate()
    {
        Animator.SetFloat("Direction", (int)facing);
        HandAnchor.transform.eulerAngles = new Vector3(0, 0, (int)facing * -90);
    }

    public enum NPCType
    {
        neutral,
        friendly,
        enemy
    }

    public enum Direction
    {
        down = 0,
        left = 1,
        up = 2,
        right = 3
    }
}

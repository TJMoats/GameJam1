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
        Vector3 direction = transform.position - AIPath.steeringTarget;
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
    }

    private bool moving = false;
    private void UpdateMovement()
    {
        moving = (AIPath.velocity.magnitude > 0);
        Animator.SetBool("Moving", moving);
    }

    private void Update()
    {
        UpdateFacing();
        UpdateMovement();
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

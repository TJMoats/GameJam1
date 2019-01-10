using BehaviorDesigner.Runtime;
using Pathfinding;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BehaviorTree))]
[RequireComponent(typeof(Seeker), typeof(AIPath))]
public class NPCController : CharacterController
{
    [SerializeField]
    private NPCType type;
    public NPCType Type
    {
        get => type;
        set => type = value;
    }

    public override bool Moving
    {
        get => AIPath.velocity.magnitude > 0;
    }
    public override Vector2 MovementDirection
    {
        get => transform.position - AIPath.steeringTarget;
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

    private CharacterSpriteAnimationController animationController;
    protected CharacterSpriteAnimationController AnimationController
    {
        get
        {
            if (animationController == null)
            {
                animationController = GetComponentInChildren<CharacterSpriteAnimationController>();
            }
            return animationController;
        }
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
        AIPath.enableRotation = false;
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

    }

    public enum NPCType
    {
        neutral,
        friendly,
        enemy
    }

}

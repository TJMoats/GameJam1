using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tactical;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatComponent : Entity, IAttackAgent, IDamageable
{
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

    private NPCController npcController;
    protected NPCController NPCController
    {
        get
        {
            if (npcController == null)
            {
                npcController = GetComponent<NPCController>();
            }
            return npcController;
        }
    }

    #region IDamageable
    [SerializeField, TitleGroup("Health")]
    private float maxHealth = 5;
    public float MaxHealth
    {
        get => maxHealth;
        private set => MaxHealth = value;
    }

    [SerializeField, ProgressBar(0, "MaxHealth", 0, 1, 0), TitleGroup("Health")]
    private float currentHealth;
    public float CurrentHealth
    {
        get => currentHealth;
        private set => currentHealth = value;
    }

    public void Damage(float _amount)
    {
        CurrentHealth -= _amount;
    }

    public bool IsAlive()
    {
        return CurrentHealth <= 0;
    }
    #endregion

    #region IAttackAgent
    [SerializeField, TitleGroup("Combat")]
    private bool canAttack = true;
    public bool CanAttack()
    {
        return canAttack && remainingAttackCooldown <= 0;
    }

    [SerializeField, Range(1, 10), TitleGroup("Combat")]
    private float attackCooldown = 3;
    public float AttackCooldown
    {
        get => attackCooldown;
    }

    [SerializeField, ReadOnly, TitleGroup("Combat"), ShowIf("CoolingDown"), ProgressBar(0, "AttackCooldown", 1, 0, 0)]
    private float remainingAttackCooldown = 0;

    public bool CoolingDown
    {
        get => remainingAttackCooldown >= 0;
    }

    public void Attack(Vector3 _targetPosition)
    {
        if (CanAttack())
        {
            TriggerAttack();

            remainingAttackCooldown = AttackCooldown;
        }
    }

    private bool attacking = false;

    [Button]
    public void TriggerAttack()
    {
        attacking = true;
        Animator.SetTrigger("Attack");
        Weapon.TriggerAttack();
    }

    public void FinishAttack()
    {
        attacking = false;
    }

    [SerializeField, Range(90, 360)]
    private float attackAngle = 90f;
    public float AttackAngle()
    {
        return attackAngle;
    }

    [SerializeField, Range(1, 10)]
    private float attackRange = 1f;
    public float AttackRange
    {
        get => attackRange;
    }
    public float AttackDistance()
    {
        return attackRange;
    }

    [SerializeField, Range(1, 25)]
    private float visionRange = 10f;
    public float VisionRange
    {
        get => visionRange;
    }
    
    [SerializeField, PreviewField]
    private Weapon weapon;
    public Weapon Weapon
    {
        get
        {
            if (weapon == null)
            {
                weapon = GetComponentInChildren<Weapon>();
            }
            return weapon;
        }
    }
    #endregion

    private void Awake()
    {
        CurrentHealth = MaxHealth;
    }

    private void Start()
    {
        InitializeBehaviorTree();
    }

    private void Update()
    {
        if (remainingAttackCooldown > 0)
        {
            remainingAttackCooldown -= Time.deltaTime;
        }
    }

    private void InitializeBehaviorTree()
    {
        if (BehaviorTree != null)
        {
            SharedFloat visionRange = VisionRange;
            SharedFloat attackRange = AttackRange;

            BehaviorTree.SetVariable("VisionRange", visionRange);
            BehaviorTree.SetVariable("AttackRange", attackRange);
        }
    }

    private void OnValidate()
    {

    }
}

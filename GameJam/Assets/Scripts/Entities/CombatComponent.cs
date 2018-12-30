using BehaviorDesigner.Runtime.Tactical;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatComponent : SerializedMonoBehaviour, IAttackAgent, IDamageable
{
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

    [SerializeField, TitleGroup("Combat")]
    private bool canAttack = true;
    public bool CanAttack()
    {
        return canAttack && RemainingAttackCooldown <= 0;
    }

    [SerializeField, Range(1, 10), TitleGroup("Combat")]
    private float attackCooldown = 3;
    public float AttackCooldown
    {
        get => attackCooldown;
        private set => attackCooldown = value;
    }

    [SerializeField, ReadOnly, TitleGroup("Combat"), ShowIf("CoolingDown"), ProgressBar(0, "AttackCooldown", 1, 0, 0)]
    private float remainingAttackCooldown = 0;
    public float RemainingAttackCooldown
    {
        get => remainingAttackCooldown;
        private set => remainingAttackCooldown = value;
    }

    public bool CoolingDown
    {
        get => remainingAttackCooldown >= 0;
    }

    public void Attack(Vector3 _targetPosition)
    {
        if (CanAttack())
        {
            RemainingAttackCooldown = AttackCooldown;
        }
    }

    [SerializeField]
    private float attackAngle = 90f;
    public float AttackAngle()
    {
        return attackAngle;
    }

    private float attackRange = 1f;
    public float AttackDistance()
    {
        return attackRange;
    }
    
    public void Damage(float _amount)
    {
        CurrentHealth -= _amount;
    }

    public bool IsAlive()
    {
        return CurrentHealth <= 0;
    }

    private void Update()
    {
        if (RemainingAttackCooldown > 0)
        {
            RemainingAttackCooldown -= Time.deltaTime;
        }    
    }

    private void OnValidate()
    {

    }
}

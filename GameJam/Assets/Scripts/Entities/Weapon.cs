using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Entity
{
    public enum WeaponType
    {
        melee,
        ranged,
        magical
    }

    public enum AttackStyle
    {
        thrust,
        swing,
        smash,
        charge,
        flail,
        shoot,
    }
    
    [SerializeField]
    private WeaponType type;
    public WeaponType Type
    {
        get => type;
    }

    [SerializeField]
    private AttackStyle style;
    public AttackStyle Style
    {
        get => style;
    }

    [SerializeField]
    private float weaponReach = 2f;
    public float WeaponReach
    {
        get => weaponReach;
    }

    [SerializeField, PreviewField]
    private Projectile projectile;
    public Projectile Projectile
    {
        get => projectile;
    }

    private bool attacking = false;

    public void TriggerAttack()
    {
        attacking = true;
    }

    public void FinishAttack()
    {
        attacking = false;
    }
}

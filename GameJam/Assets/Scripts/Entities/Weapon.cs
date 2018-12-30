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

    private Transform projectileAnchor;
    public Transform ProjectileAnchor
    {
        get
        {
            if (projectileAnchor == null)
            {
                projectileAnchor = transform.Find("ProjectileAnchor");
                if (projectileAnchor == null)
                {
                    GameObject go = new GameObject("ProjectileAnchor");
                    go.transform.SetParent(transform);
                    go.transform.localPosition = Vector3.zero;
                    projectileAnchor = go.transform;
                }
            }
            return projectileAnchor;
        }
    }

    private bool attacking = false;

    public void TriggerAttack()
    {
        attacking = true;
        Animator.SetTrigger("Attack");
    }

    public void FinishAttack()
    {
        if (Projectile != null)
        {
            Projectile shot = Instantiate(Projectile, ProjectileAnchor.position, ProjectileAnchor.rotation, null);
        }
        attacking = false;
    }
}

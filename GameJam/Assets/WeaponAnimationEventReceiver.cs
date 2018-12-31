using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimationEventReceiver : SerializedMonoBehaviour
{
    private Weapon weapon;
    public Weapon Weapon
    {
        get
        {
            if (weapon == null)
            {
                weapon = GetComponentInParent<Weapon>();
            }
            return weapon;
        }
    }

    public void FinishAttack()
    {
        Weapon.FinishAttack();
    }
}

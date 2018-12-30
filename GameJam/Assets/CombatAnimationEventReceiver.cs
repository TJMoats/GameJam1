using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatAnimationEventReceiver : SerializedMonoBehaviour
{
    private CombatComponent combatComponent;
    public CombatComponent CombatComponent
    {
        get
        {
            if (combatComponent == null)
            {
                combatComponent = GetComponentInParent<CombatComponent>();
            }
            return combatComponent;
        }
    }

    public void FinishAttack()
    {
        combatComponent.FinishAttack();
    }
}

using BehaviorDesigner.Runtime;
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCController : Entity
{
    public enum NPCType
    {
        neutral,
        friendly,
        enemy
    }

    [SerializeField]
    private NPCType type;
    public NPCType Type
    {
        get => type;
        set => type = value;
    }

    private CombatComponent combatComponent;
    public CombatComponent CombatComponent
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

    private void OnValidate()
    {
        
    }
}

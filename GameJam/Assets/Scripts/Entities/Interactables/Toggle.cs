using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Toggle : InteractableComponent
{
    [SerializeField]
    private bool canToggle = true;
    private bool isActive = false;

    [SerializeField]
    protected GameObject toToggle;
    protected virtual GameObject ToToggle
    {
        get => toToggle;
    }

    public override void ReceiveInteraction(InteractionComponent _target)
    {
        ToggleGo();
    }

    protected override void Awake()
    {
        base.Awake();
        if (toToggle != null)
        {
            isActive = toToggle.activeSelf;
        }
    }

    [Button, LabelText("Toggle")]
    public void ToggleGo()
    {
        if (canToggle && toToggle != null)
        {
            isActive = !isActive;
            toToggle.SetActive(isActive);
        }
    }
}

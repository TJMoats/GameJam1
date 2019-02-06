using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : InteractableComponent
{
    [SerializeField]
    private bool canToggle = true;
    private bool isOpen = false;

    private GameObject closedStateContainer;
    protected GameObject ClosedStateContainer
    {
        get
        {
            if (closedStateContainer == null)
            {
                closedStateContainer = transform.Find("Sprites/Closed")?.gameObject;
            }
            return closedStateContainer;
        }
    }

    private void Start()
    {
        isOpen = ClosedStateContainer.activeSelf;
    }

    public override void ReceiveInteraction(InteractionComponent _target)
    {
        ToggleDoorState();
    }

    [Button]
    public void ToggleDoorState()
    {
        if (canToggle)
        {
            isOpen = !isOpen;
            ClosedStateContainer.SetActive(isOpen);
        }
    }
}

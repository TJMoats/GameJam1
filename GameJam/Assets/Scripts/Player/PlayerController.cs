using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SerializedMonoBehaviour
{
    public Rigidbody2D Rigidbody2D;
    public float MovementSpeed = 10.0f;
    private Vector2 playerFacing;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void UpdateMovement(InputEventData data)
    {
        
    }

    public void UpdateActions(InputEventData data)
    {
        // State machine this?
        if (data.Handled)
            return;

        // Movement
        if (Rigidbody2D != null)
        {
            Vector2 newPosition = Rigidbody2D.position + data.DPadDirection * MovementSpeed;
            Rigidbody2D.MovePosition(newPosition);
            playerFacing = data.DPadDirection;
        }

        // Actions
        if (data.KeyStateMap[InputKeys.InteractionAction] == InputKeyState.DownThisFrame)
        {
            PerformInteractionAction();
        }
        //else if (data.KeyStateMap[InputKeys.BasicAction] == InputKeyState.DownThisFrame)
        //{
        //    PerformBasicAction();
        //}
        //else if (data.KeyStateMap[InputKeys.SecondaryAction] == InputKeyState.DownThisFrame)
        //{
        //    PerformSecondaryAction();
        //}
        //else if (data.KeyStateMap[InputKeys.ShieldAction] == InputKeyState.DownThisFrame)
        //{
            
        //}
    }

    private void PerformInteractionAction()
    {
        // Interaction logic - Player -> Object/NPC
        Vector2 targetPosition = Rigidbody2D.position;
        Debug.DrawLine(Rigidbody2D.position, Rigidbody2D.position + playerFacing);
        var hitTarget = Physics2D.Raycast(Rigidbody2D.position, playerFacing, 1.0f);
        InteractionComponent interactionTarget = hitTarget.collider.GetComponent<InteractionComponent>();
        if (interactionTarget != null)
        {
            
        }
    }
}
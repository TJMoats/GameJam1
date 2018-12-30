using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SerializedMonoBehaviour
{
    public Rigidbody2D Rigidbody2D;
    public float MovementSpeed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void UpdateMovement(InputEventData data)
    {
        if (data.Handled)
            return;
        if (Rigidbody2D != null)
        {
            Vector2 newPosition = Rigidbody2D.position + data.DPadDirection * MovementSpeed;
            Rigidbody2D.MovePosition(newPosition);
        }
    }
}
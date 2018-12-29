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

    // Update is called once per frame
    void FixedUpdate()
    {
        // Update Player movement
        UpdateInput();
    }

    private void UpdateInput()
    {
        if (Rigidbody2D != null)
        {
            Vector2 input = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            Vector2 newPosition = Rigidbody2D.position + input * MovementSpeed;
            Rigidbody2D.MovePosition(newPosition);
        }
    }
}

using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSpriteAnimationController : SerializedMonoBehaviour
{
    private Animator spriteAnimator;
    public Animator SpriteAnimator
    {
        get
        {
            if (spriteAnimator == null)
            {
                spriteAnimator = GetComponent<Animator>();
            }
            return spriteAnimator;
        }
    }

    private CharacterController characterController;
    protected CharacterController CharacterController
    {
        get
        {
            if (characterController == null)
            {
                characterController = GetComponentInParent<CharacterController>();
            }
            return characterController;
        }
    }

    [SerializeField]
    Direction facing = Direction.down;
    private void SetDirection(Direction _direction)
    {
        facing = _direction;
        SpriteAnimator.SetFloat("Direction", (int)facing);
    }

    private void SetDirection(Vector2 _direction)
    {
        Direction newFacing;
        if (Mathf.Abs(_direction.x) > Mathf.Abs(_direction.y))
        {
            // Left or right
            newFacing = _direction.x > 0 ? Direction.left : Direction.right;
        }
        else
        {
            // Up or down
            newFacing = _direction.y > 0 ? Direction.down : Direction.up;
        }
            SetDirection(newFacing);
    }

    private void UpdateMovement()
    {
        SpriteAnimator.SetBool("Moving", CharacterController.Moving);
    }

    private void UpdateFacing()
    {
        SetDirection(CharacterController.MovementDirection);
    }

    private void Update()
    {
        UpdateMovement();
        if (CharacterController.Moving)
        {
            UpdateFacing();
        }
    }
}


public enum Direction
{
    down = 0,
    left = 1,
    up = 2,
    right = 3
}
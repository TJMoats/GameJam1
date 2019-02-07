using Sirenix.OdinInspector;
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
    private Direction facing = Direction.down;
    public Direction Facing
    {
        get => facing;
    }

    private Vector2 facingVector;
    public Vector2 FacingVector
    {
        get => facingVector;
    }
    private void SetDirection(Direction _direction)
    {
        facing = _direction;
        SpriteAnimator.SetFloat("Direction", (int)facing);
    }

    private void SetDirection(Vector2 _direction)
    {
        facingVector = _direction * -1;
        Direction newFacing;
        if (Mathf.Abs(facingVector.x) > Mathf.Abs(facingVector.y))
        {
            // Left or right
            newFacing = facingVector.x < 0 ? Direction.left : Direction.right;
        }
        else
        {
            // Up or down
            newFacing = facingVector.y < 0 ? Direction.down : Direction.up;
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
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : CharacterController
{
    [SerializeField]
    private InputMaster controls;
    [SerializeField]
    private Vector2 movementDirection = Vector2.zero;
    [SerializeField]
    private float accelerationSpeed = 3;
    [SerializeField]
    private float movementSpeed = 10;

    public override bool Moving
    {
        get => movementDirection != Vector2.zero;
    }

    public override Vector2 MovementDirection
    {
        get => movementDirection * -1;
    }

    private void Awake()
    {
        controls.Player.Attack.performed += ctx => AttackAction();
        controls.Player.Interact.performed += ctx => InteractAction();
        controls.Player.Movement.performed += ctx => MoveAction(ctx.ReadValue<Vector2>());
    }

    private void FixedUpdate()
    {
        Rigidbody.velocity = movementDirection * movementSpeed;
    }

    private void AttackAction()
    {
        Debug.Log("Attack!");
    }

    private void InteractAction()
    {
        Debug.Log("Interact!");
    }

    private void MoveAction(Vector2 _direction)
    {
        movementDirection = _direction;
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }
}
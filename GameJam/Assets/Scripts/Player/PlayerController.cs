using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : Entity
{
    [SerializeField]
    private InputMaster controls;
    [SerializeField]
    private Vector2 movementDirection = Vector2.zero;
    [SerializeField]
    private float accelerationSpeed = 3;
    [SerializeField]
    private float movementSpeed = 10;

    private void Awake()
    {
        controls.Player.Attack.performed += ctx => Attack();
        controls.Player.Interact.performed += ctx => Interact();
        controls.Player.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        Rigidbody.velocity = movementDirection * movementSpeed;
    }

    private void Attack()
    {
        Debug.Log("Attack!");
    }

    private void Interact()
    {
        Debug.Log("Interact!");
    }

    private void Move(Vector2 _direction)
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
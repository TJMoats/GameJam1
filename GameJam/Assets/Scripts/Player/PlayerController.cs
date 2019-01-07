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
    private float maxMovementSpeed = 10;
    private float sqrMaxMovementSpeed;

    private void Awake()
    {
        controls.Player.Attack.performed += ctx => Attack();
        controls.Player.Interact.performed += ctx => Interact();
        controls.Player.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());

        sqrMaxMovementSpeed = Mathf.Sqrt(maxMovementSpeed);
    }

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        Rigidbody.AddForce(movementDirection * accelerationSpeed);
        if (Rigidbody.velocity.sqrMagnitude > sqrMaxMovementSpeed)
        {
            Debug.Log("Slowing down!");
            Rigidbody.velocity = Rigidbody.velocity.normalized * maxMovementSpeed;
        }
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
        Debug.Log("Enabled!");
        controls.Enable();
    }

    private void OnDisable()
    {
        Debug.Log("Disabled!");
        controls.Disable();
    }
}
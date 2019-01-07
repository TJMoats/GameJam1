using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : SerializedMonoBehaviour
{
    [SerializeField]
    private InputMaster controls;

    private void Awake()
    {
        Debug.Log("Awake!");
        controls.Player.Attack.performed += ctx => Attack();
        controls.Player.Interact.performed += ctx => Interact();
        controls.Player.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
    }

    private void Start()
    {

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
        Debug.Log($"Move ${_direction}");
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
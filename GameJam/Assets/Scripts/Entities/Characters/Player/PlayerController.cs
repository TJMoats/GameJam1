using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Experimental.Input;
using static UnityEngine.Experimental.Input.InputAction;

public class PlayerController : CharacterController
{
    private static PlayerController instance;
    public static PlayerController Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<PlayerController>();
            }
            return instance;
        }
    }

    #region Components
    private InteractionComponent interactionComponent;
    public InteractionComponent InteractionComponent
    {
        get
        {
            if (interactionComponent == null)
            {
                interactionComponent = gameObject.GetComponent<InteractionComponent>();
            }
            return interactionComponent;
        }
    }

    private CharacterSpriteAnimationController animationController;
    public CharacterSpriteAnimationController AnimationController
    {
        get
        {
            if (animationController == null)
            {
                animationController = GetComponentInChildren<CharacterSpriteAnimationController>();
            }
            return animationController;
        }
    }
    #endregion

    [SerializeField]
    private InputMaster controls;
    [SerializeField]
    private Vector2 movementDirection = Vector2.zero;
    [SerializeField]
    private float accelerationSpeed = 3;
    [SerializeField]
    private float movementSpeed = 10;
    public float MovementSpeed
    {
        get => movementSpeed * (Sprinting ? 1.5f : 1f);
    }

    public bool Sprinting
    {
        get => InputSystem.GetDevice<Keyboard>().leftShiftKey.isPressed;
    }

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
        controls.Player.Movement.cancelled += ctx => MoveAction(ctx.ReadValue<Vector2>());
        LayerHelper.AssignLayer(gameObject, LayerHelper.Player);
    }

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        Rigidbody.velocity = movementDirection * MovementSpeed;
    }

    private void AttackAction()
    {
        Debug.Log("Attack!");
    }

    private void InteractAction()
    {
        InteractableComponent interactableComponent = InteractionComponent.CheckForInteractable(AnimationController.FacingVector);
        if (interactableComponent != null)
        {
            Debug.Log($"Interacting with {interactableComponent.name}", interactableComponent.gameObject);
            interactableComponent.ReceiveInteraction(InteractionComponent);
        }
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
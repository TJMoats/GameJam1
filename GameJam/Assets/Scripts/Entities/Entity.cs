using Sirenix.OdinInspector;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Entity : SerializedMonoBehaviour
{
    private void Awake()
    {

    }

    private SpriteRenderer spriteRenderer;
    protected SpriteRenderer SpriteRenderer
    {
        get
        {
            if (spriteRenderer == null)
            {
                spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            }
            return spriteRenderer;
        }
    }

    private Animator animator;
    protected Animator Animator
    {
        get
        {
            if (animator == null)
            {
                animator = transform.Find("Sprite")?.gameObject.GetComponent<Animator>();
            }
            return animator;
        }
    }

    private new Rigidbody2D rigidbody;
    protected Rigidbody2D Rigidbody
    {
        get
        {
            if (rigidbody == null)
            {
                rigidbody = GetComponent<Rigidbody2D>();
            }
            return rigidbody;
        }
    }

    private new Collider2D collider;
    protected Collider2D Collider
    {
        get
        {
            if (collider == null)
            {
                collider = GetComponent<Collider2D>();
                if (collider == null)
                {
                    collider = GetComponentInChildren<Collider2D>();
                }
            }
            return collider;
        }
    }
}

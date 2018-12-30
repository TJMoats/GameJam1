using Sirenix.OdinInspector;
using UnityEngine;

public class Entity : SerializedMonoBehaviour
{
    [SerializeField]
    private Sprite sprite;
    protected Sprite Sprite
    {
        get
        {
            if (sprite == null)
            {
                sprite = GetComponentInChildren<Sprite>();
            }
            return sprite;
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

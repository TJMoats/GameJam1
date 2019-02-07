using Sirenix.OdinInspector;
using UnityEngine;

public class InteractionComponent : SerializedMonoBehaviour
{
    [SerializeField]
    private float interactionRange = 1f;

    public void InteractWith(InteractableComponent _target)
    {
        _target.ReceiveInteraction(this);
    }

    Vector2 direction;
    public InteractableComponent CheckForInteractable(Vector2 _direction)
    {
        direction = _direction;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, _direction, interactionRange, LayerHelper.LayerUnion(LayerHelper.Interactable));
        if (hit.collider)
        {
            InteractableComponent interactable = hit.collider?.gameObject?.GetComponent<InteractableComponent>();
            if (interactable != null)
            {
                return interactable;
            }
        }
        return null;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, direction);
    }
}

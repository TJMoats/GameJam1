using Sirenix.OdinInspector;

public class InteractableComponent : Entity
{
    protected virtual void Awake()
    {
        LayerHelper.AssignLayer(gameObject, LayerHelper.Interactable, true);
    }

    public virtual void ReceiveInteraction(InteractionComponent _source)
    {
        throw new System.NotImplementedException();
    }
}

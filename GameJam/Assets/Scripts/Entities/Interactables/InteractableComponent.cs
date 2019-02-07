using Sirenix.OdinInspector;

public class InteractableComponent : Entity
{
    private void Awake()
    {
        LayerHelper.AssignLayer(gameObject, LayerHelper.Interactable, true);
    }

    public virtual void ReceiveInteraction(InteractionComponent _source)
    {
        throw new System.NotImplementedException();
    }
}

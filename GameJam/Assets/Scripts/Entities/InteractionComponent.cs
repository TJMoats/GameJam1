using Sirenix.OdinInspector;

public class InteractionComponent : SerializedMonoBehaviour, ICanInteract
{
    public void InteractWith(ICanInteract _target)
    {
        throw new System.NotImplementedException();
    }

    public void ReceiveInteraction(ICanInteract _target)
    {
        throw new System.NotImplementedException();
    }
}

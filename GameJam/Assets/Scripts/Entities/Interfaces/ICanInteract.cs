using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICanInteract 
{
    void InteractWith(ICanInteract _target);
    void ReceiveInteraction(ICanInteract _target);
}
using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trigger : SerializedMonoBehaviour
{
    private new Collider2D collider2D;
    protected Collider2D Collider2D
    {
        get
        {
            if (collider2D == null)
            {
                collider2D = GetComponent<Collider2D>();
            }
            return collider2D;
        }
    }

    private void Awake()
    {
        Collider2D.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D _collision)
    {
        CharacterController cc = _collision?.gameObject?.GetComponent<CharacterController>();
        if (cc != null)
        {
            OnCharacterEnter(cc);
        }
    }

    private void OnTriggerExit2D(Collider2D _collision)
    {
        CharacterController cc = _collision?.gameObject?.GetComponent<CharacterController>();
        if (cc != null)
        {
            OnCharacterExit(cc);
        }
    }

    private void OnTriggerStay2D(Collider2D _collision)
    {
        CharacterController cc = _collision?.gameObject?.GetComponent<CharacterController>();
        if (cc != null)
        {
            OnCharacterStay(cc);
        }
    }

    protected virtual void OnCharacterEnter(CharacterController _character)
    {

    }

    protected virtual void OnCharacterExit(CharacterController _character)
    {
        
    }

    protected virtual void OnCharacterStay(CharacterController _character)
    {
        
    }
}

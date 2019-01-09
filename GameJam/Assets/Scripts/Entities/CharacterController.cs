using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterController : Entity
{
    public abstract bool Moving
    {
         get;
    }

    public abstract Vector2 MovementDirection
    {
        get;
    }
}

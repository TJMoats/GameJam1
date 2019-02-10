using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Toggle
{
    protected override GameObject ToToggle
    {
        get
        {
            if (toToggle == null)
            {
                toToggle = transform.Find("Sprites/Closed")?.gameObject;
            }
            return toToggle;
        }
    }
}
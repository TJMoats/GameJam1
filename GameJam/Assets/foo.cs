using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foo : MonoBehaviour
{
    [Button]
    public void FixMe()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Vector3 v = transform.GetChild(i).transform.position;
            v.x = Mathf.Round(v.x);
            v.y = Mathf.Round(v.y);
            v.z = 0;
            transform.GetChild(i).transform.position = v;
        }
    }
}

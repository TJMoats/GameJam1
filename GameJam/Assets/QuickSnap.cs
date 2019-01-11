using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSnap : SerializedMonoBehaviour
{
    private void OnValidate()
    {
        CleanUp();
    }

    public void Snap()
    {
        for (int y = 0; y < transform.childCount; y++)
        {
            Transform row = transform.GetChild(y);
            row.localPosition = new Vector3(0, y * -1, 0);
            row.name = $"Row_{y}";
            Reorder(row);
            for (int x = 0; x < row.childCount; x++)
            {
                Transform cell = row.GetChild(x);
                cell.localPosition = new Vector3(x, 0, 0);
                cell.name = $"Sprite_{x}_{y}";
            }
        }
    }

    public void Reorder(Transform _transform)
    {
        for (int i = 0; i < _transform.childCount - 1; i++)
        {
            for (int j = 0; j < _transform.childCount - i - 1; j++)
            {
                if (_transform.GetChild(j).position.x > _transform.GetChild(j + 1).position.x)
                {
                    Swap(_transform.GetChild(j), _transform.GetChild(j + 1));
                }
            }
        }
    }

    private void Swap(Transform _a, Transform _b)
    {
        _a.SetSiblingIndex(_a.GetSiblingIndex() + 1);
    }

    [Button]
    public void CleanUp()
    {
        Snap();
    }
}

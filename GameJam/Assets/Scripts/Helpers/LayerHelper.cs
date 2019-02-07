using UnityEngine;


public class LayerHelper
{
    public static LayerMask All
    {
        get => ~0;
    }
    public static LayerMask None
    {
        get => 0;
    }
    public static LayerMask Default
    {
        get => EnsureLayerId("Default");
    }
    public static LayerMask TransparentFX
    {
        get => EnsureLayerId("TransparentFX");
    }
    public static LayerMask IgnoreRaycast
    {
        get => EnsureLayerId("Ignore Raycast");
    }
    public static LayerMask Water
    {
        get => EnsureLayerId("Water");
    }
    public static LayerMask UI
    {
        get => EnsureLayerId("UI");
    }
    public static LayerMask Terrain
    {
        get => EnsureLayerId("Terrain");
    }
    public static LayerMask Walkable
    {
        get => EnsureLayerId("Walkable");
    }
    public static LayerMask NonWalkable
    {
        get => EnsureLayerId("Non Walkable");
    }
    public static LayerMask Obstacle
    {
        get => EnsureLayerId("Obstacle");
    }
    public static LayerMask Interactable
    {
        get => EnsureLayerId("Interactable");
    }
    public static LayerMask Player
    {
        get => EnsureLayerId("Player");
    }

    public static string LayerName(int _layerId)
    {
        return LayerMask.LayerToName(_layerId);
    }
    public static bool InLayer(GameObject _go, LayerMask _layer)
    {
        return InLayer(_go.layer, _layer);
    }
    public static bool InLayer(LayerMask _layerToCheck, LayerMask _layer)
    {
        return (_layer & (1 << _layerToCheck)) != 0;
    }
    public static void AssignLayer(GameObject _go, LayerMask _layer, bool _includeChildren = false)
    {
        _go.layer = _layer;
        if (_includeChildren)
        {
            foreach (Transform child in _go.transform)
            {
                AssignLayer(child.gameObject, _layer, _includeChildren);
            }
        }
    }
    public static void AssignLayer(GameObject _go, string _layerName, bool _includeChildren = false)
    {
        LayerMask layer = LayerMask.NameToLayer(_layerName);
        if (layer.value == 0)
        {
            Debug.Log($"Attempted to set invalid layer {_layerName}");
        }
        else
        {
            AssignLayer(_go, layer, _includeChildren);
        }
    }
    public static LayerMask InverseLayer(LayerMask _layerMask)
    {
        LayerMask inverseMask = new LayerMask();
        for (int i = 0; i < 32; i++)
        {
            string layerName = LayerMask.LayerToName(i);
            if (layerName != "")
            {
                if (!InLayer(_layerMask, LayerMask.GetMask(layerName)))
                {
                    inverseMask |= (1 << LayerMask.NameToLayer(layerName));
                }
            }
        }
        return inverseMask;
    }
    public static LayerMask LayerUnion(params LayerMask[] _layerMasks)
    {
        LayerMask unionMask = new LayerMask();
        for (int i = 0; i < _layerMasks.Length; i++)
        {
            unionMask = (unionMask | (1 << _layerMasks[i]));
        }
        return unionMask;
    }
    public static LayerMask EnsureLayerId(string _layerName)
    {
        int layer = LayerMask.NameToLayer(_layerName);
        if (layer == -1)
        {
            throw new System.Exception($"Missing layer {_layerName}.");
        }
        return layer;
    }
    public static LayerMask EnsureLayer(string _layerName)
    {
        int layer = LayerMask.GetMask(_layerName);
        if (layer == -1)
        {
            throw new System.Exception($"Missing layer {_layerName}.");
        }
        return layer;
    }
}
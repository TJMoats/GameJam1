using System;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Various extension to Unity types.
/// </summary>
public static partial class ExtensionMethods
{
    public static T GetOrAddComponent<T>(this GameObject _target, out T _component) where T : Component
    {
        _component = _target.GetComponent<T>();
        if (_component == null)
        {
            _component = _target.AddComponent<T>();
        }
        return _component;
    }

    public static T GetOrAddComponent<T>(this GameObject _target) where T : Component
    {
        return GetOrAddComponent(_target, out T component);
    }
}


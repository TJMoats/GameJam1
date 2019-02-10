using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class ScriptableObjectDictionary<T> : BaseDictionary where T : SerializedScriptableObject
{
    [SerializeField]
    private Dictionary<string, T> _dictionary = new Dictionary<string, T>();
    public Dictionary<string, T> Dictionary
    {
        get
        {
            if (_dictionary == null)
            {
                _dictionary = new Dictionary<string, T>();
                Populate();
            }

            return _dictionary;
        }
        protected set => _dictionary = value;
    }
    public List<T> List
    {
        get => Dictionary.Values.ToList();
    }

    public override void Populate()
    {
        T[] resourceList = Resources.FindObjectsOfTypeAll<T>();
        foreach (T resource in resourceList)
        {
            if (!Dictionary.ContainsKey(resource.name))
            {
                Dictionary.Add(resource.name, resource);
            }
            else
            {
                Dictionary[resource.name] = resource;
            }
        }
    }

    [Button]
    public void RePopulate()
    {
        Dictionary = null;
        Populate();
    }

    public ValueDropdownList<T> OdinDropdownValues()
    {
        ValueDropdownList<T> dropdownValues = new ValueDropdownList<T>
            {
                { "None", null }
            };

        foreach (KeyValuePair<string, T> kvp in Dictionary)
        {
            dropdownValues.Add(kvp.Key, kvp.Value);
        }
        return dropdownValues;
    }
}

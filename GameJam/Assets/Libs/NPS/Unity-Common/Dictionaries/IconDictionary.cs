using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System;

namespace NPS
{
    [CreateAssetMenu(fileName = "Icon Dictionary", menuName = "Dictionary/Icon Dictionary")]
    public class IconDictionary : BaseDictionary
    {
        [SerializeField]
        private Dictionary<string, Sprite> _dict;
        private Dictionary<string, Sprite> Dict
        {
            get
            {
                if (_dict == null)
                {
                    _dict = new Dictionary<string, Sprite>();
                }
                return _dict;
            }
        }

        public Sprite GetIcon(string _iconKey)
        {
            if (Dict.ContainsKey(_iconKey))
            {
                return Dict[_iconKey];
            }
            Dict[_iconKey] = null;
            Debug.Log($"Missing Icon for {_iconKey}");
            return null;
        }

        public override void Populate()
        {
            // Do nothing.
        }
    }
}
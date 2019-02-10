using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPS
{
    public class TabViewPanel : SerializedMonoBehaviour
    {
        private GameObject activeBorder;
        public bool IsActive
        {
            get
            {
                return gameObject.activeSelf;
            }
            set
            {
                gameObject.SetActive(value);
            }
        }
    }
}
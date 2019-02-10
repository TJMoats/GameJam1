using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NPS
{
    public class TabViewButton : SerializedMonoBehaviour
    {
        private Button _button;
        public Button Button
        {
            get
            {
                if (_button == null)
                {
                    _button = GetComponent<Button>();
                }
                return _button;
            }
        }

        private GameObject _activeBorder;
        private GameObject ActiveBorder
        {
            get
            {
                if (_activeBorder == null)
                {
                    _activeBorder = transform.Find("ActiveBorder").gameObject;
                }
                return _activeBorder;
            }
        }

        public bool IsActive
        {
            get => ActiveBorder.activeSelf;
            set => ActiveBorder.SetActive(value);
        }

        private TabViewManager _manager;
        private TabViewManager Manager
        {
            get
            {
                if (_manager == null)
                {
                    _manager = GetComponentInParent<TabViewManager>();
                }
                return _manager;
            }
        }

        private void Start()
        {
            // Button.onClick.AddListener(OnButtonClick);
        }

        private void OnButtonClick()
        {
            Manager.SetActiveTab(this);
        }
    }
}
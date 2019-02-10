using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace NPS
{
    public class TabViewManager : SerializedMonoBehaviour
    {
        private List<TabViewButton> tabButtons;
        private List<TabViewPanel> tabPanels;

        private void Awake()
        {
            tabButtons = GetComponentsInChildren<TabViewButton>().ToList();
            tabPanels = GetComponentsInChildren<TabViewPanel>().ToList();
        }

        private void Start()
        {
            SetActiveTab(0);
        }

        public void SetActiveTab(int _tabIndex)
        {
            for (int x = 0; x < tabButtons.Count; x++)
            {
                tabButtons[x].IsActive = (x == _tabIndex);
            }
            for (int x = 0; x < tabPanels.Count; x++)
            {
                tabPanels[x].IsActive = (x == _tabIndex);
            }
        }

        public void SetActiveTab(TabViewButton _buttonRef)
        {
            int tabIndex = tabButtons.IndexOf(_buttonRef);
            SetActiveTab(tabIndex);
            onTabChangeCallback?.Invoke(tabIndex);
        }

        [HideInPlayMode]
        [HideInEditorMode]
        public Action<int> onTabChangeCallback;
    }
}
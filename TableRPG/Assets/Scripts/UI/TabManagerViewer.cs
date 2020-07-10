using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TableRPG
{
    public class TabManagerViewer : MonoBehaviour
    {
        [SerializeField]
        private List<TabObject> tabs;

        private int selectedTabId = -1;

        #region UI
        public void SelectTab(int id)
        {
            if (this.selectedTabId == id) return;

            UnselectLastTab();
            this.selectedTabId = id;
            SelectCurrentTab();
        }
        #endregion

        #region private methods

        private void UnselectLastTab()
        {
            if (!AtRange(this.tabs, this.selectedTabId)) return;
            var tab = this.tabs[this.selectedTabId];
            tab.EnableContent(false);
        }

        private void SelectCurrentTab()
        {
            if (!AtRange(this.tabs, this.selectedTabId)) return;
            var tab = this.tabs[this.selectedTabId];
            tab.EnableContent(true);
        }

        private bool AtRange(List<TabObject> list, int id)
        {
            return id >= 0 && id < list.Count;
        }
        #endregion
    }

    [System.Serializable]
    public class TabObject
    {
        public TabButtonController button;
        public GameObject content;

        public void EnableContent(bool value)
        {
            this.button.EnableBackgroundImage(value);
            this.content.SetActive(value);
        }
    }
}
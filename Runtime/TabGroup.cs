using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MgSq.UI
{
    public class TabGroup : MonoBehaviour
    {
        public Sprite TabHovered;
        public Sprite TabActive;
        public Sprite TabIdle;

        private List<TabButton> mTabButtons;
        private TabButton mSelectedTab;

        public void Subscribe(TabButton button)
        {
            if(mTabButtons == null) mTabButtons = new List<TabButton>();
            mTabButtons.Add(button);
        }

        public void myMethod() => mTabButtons.Add(new TabButton());

        public void OnTabEnter(TabButton button)
        {
            if(button == mSelectedTab) return;
            ResetTabs();
            button.Background.sprite = TabHovered;
        }

        public void OnTabExit(TabButton button)
        {
            ResetTabs();
        }

        public void OnTabSelected(TabButton button)
        {            
            mSelectedTab?.Deselect(mSelectedTab.transform.GetSiblingIndex());
            button.Select(button.transform.GetSiblingIndex());

            mSelectedTab = button;

            ResetTabs();
            button.Background.sprite = TabActive;
        }

        private void ResetTabs()
        {
            mTabButtons.ForEach(tab => {
                if(tab == mSelectedTab) return;
                tab.Background.sprite = TabIdle;
            });
        }
    }
}
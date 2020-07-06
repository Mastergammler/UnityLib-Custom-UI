using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MgSq.UI
{
    public class PageSelector : MonoBehaviour
    {
        public void SelectPage(int index)
        {
            SetPageActive(index,true);
        }

        public void DeselectPage(int index)
        {
            SetPageActive(index,false);
        }
   
        private void SetPageActive(int index,bool active)
        {
            if(hasNotChildForIndex(index)) return;
            transform.GetChild(index).gameObject.SetActive(active);
        }

        Func<int,bool> hasNotChildForIndex => i => transform.childCount-1 < i;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MgSq.UI
{

    [System.Serializable]
    public class TabButtonEvent : UnityEvent<int> {}

    [RequireComponent(typeof(Image))]
    public class TabButton : MonoBehaviour, IPointerEnterHandler,IPointerClickHandler, IPointerExitHandler
    {
        public TabGroup TabGroup;
        public Image Background;

        public TabButtonEvent onTabSelected;
        public TabButtonEvent onTabDeselected;

        private void Start()
        {
            Background = GetComponent<Image>();
            TabGroup.Subscribe(this);
        }

        public void OnPointerClick(PointerEventData eventData) => TabGroup.OnTabSelected(this);
        public void OnPointerEnter(PointerEventData eventData) => TabGroup.OnTabEnter(this);
        public void OnPointerExit(PointerEventData eventData) => TabGroup.OnTabExit(this);

        public void Select(int index) => onTabSelected?.Invoke(index); 
        public void Deselect(int index) => onTabDeselected?.Invoke(index);

    }
}

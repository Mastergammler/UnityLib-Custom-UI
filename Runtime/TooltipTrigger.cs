using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace MgSq.UI
{
    public class TooltipTrigger : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public string TooltipHeader;
        public string TooltipContent;
        private  LTDescr mDelay;
        public virtual void OnPointerEnter(PointerEventData eventData)
        {
            mDelay = LeanTween.delayedCall(.5f,() => TooltipSystem.Instance.Show(TooltipContent,TooltipHeader));
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {
            LeanTween.cancel(mDelay.uniqueId);
            TooltipSystem.Instance.Hide();
        }
    }
}

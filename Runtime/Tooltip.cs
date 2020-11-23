using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace MgSq.UI
{
    [ExecuteInEditMode()]
    public class Tooltip : MonoBehaviour
    {
        public TextMeshProUGUI mHeader;
        public TextMeshProUGUI mContent;
        public LayoutElement mLayoutElement;
        public int mCharacterWrapLimit;

        private RectTransform mTooltipPivot;

        private void Awake() 
        {
          mTooltipPivot = GetComponent<RectTransform>();
        }

        public void SetText(string tooltip, string header = "")
        {
            mHeader.gameObject.SetActive(!string.IsNullOrEmpty(header));
            mHeader.text = header;
            mContent.text = tooltip;
            mLayoutElement.enabled = (mHeader.text.Length > mCharacterWrapLimit || mContent.text.Length > mCharacterWrapLimit) ? true : false;
        }

        private void Update()
        {
            if (Application.isEditor)
            {
                if (mHeader == null || mContent == null || mLayoutElement == null) return;
                int headerLength = mHeader.text.Length;
                int contentLength = mContent.text.Length;
                mLayoutElement.enabled = (headerLength > mCharacterWrapLimit || contentLength > mCharacterWrapLimit) ? true : false;
            }

            Vector2 pos = Input.mousePosition;
            float posX = pos.x / Screen.width;
            float posY = pos.y / Screen.height;
            transform.position = pos;
            mTooltipPivot.pivot = new Vector2(posX,posY);
        }


    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MgSq.UI
{
	public class TooltipSystem : MonoBehaviour
	{
		private static TooltipSystem mInstance;
		public static TooltipSystem Instance => mInstance;

		public Tooltip Tooltip;

		private void Awake()
		{
			mInstance = this;
		}

		public void Show(string content, string header = "")
		{
			mInstance.Tooltip.SetText(content, header);
			mInstance.Tooltip.gameObject.SetActive(true);
		}
		public void Hide()
		{
			mInstance.Tooltip.gameObject.SetActive(false);
		}
	}
}

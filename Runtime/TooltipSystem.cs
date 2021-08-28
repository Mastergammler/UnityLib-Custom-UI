using System;
using UnityEngine;

namespace MgSq.UI
{
	/// <summary>
	/// Simple Singleton to show an hide a tooltip. 
	/// A <see cref="Tooltip"> has to set via the inspector
	/// </summary>
	public class TooltipSystem : MonoBehaviour
	{
		private static TooltipSystem sInstance;
		public static TooltipSystem Instance => sInstance;

		public Tooltip Tooltip;

		private void Awake()
		{
			sInstance = this;
		}

		private void Start()
		{
			if (Tooltip == null) throw new NullReferenceException("Setup Error: A tooltip object has to be set via the inspector!");
		}

		public void Show(string content, string header = "")
		{
			Tooltip.SetText(content, header);
			Tooltip.gameObject.SetActive(true);
		}
		public void Hide()
		{
			Tooltip.gameObject.SetActive(false);
		}
	}
}

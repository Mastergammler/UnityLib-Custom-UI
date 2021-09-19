using System.Net;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace MgSq.UI.Inventory
{
	/// <summary>
	/// Inventory Slot button, that deactivates all mouse events. 
	/// For keyboard only use        
	/// </summary>
	public class InventorySlot : Button
	{
		private new void Awake()
		{
			navigation = new Navigation()
			{
				mode = Navigation.Mode.None
			};
		}


		// Deactivate mouse events
		public override void OnPointerEnter(PointerEventData data) { }
		public override void OnPointerExit(PointerEventData data) { }
		public override void OnPointerClick(PointerEventData data) { }
		public override void OnPointerDown(PointerEventData data) { }
		public override void OnPointerUp(PointerEventData data) { }
	}
}

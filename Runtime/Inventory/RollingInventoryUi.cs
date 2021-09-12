using System;
using System.Linq;
using UnityEngine;

namespace MgSq.UI.Inventory
{
	[RequireComponent(typeof(RollingInventoryLayout))]
	public class RollingInventoryUi : BasicInventoryUi
	{
		private RollingInventoryLayout mLayout;


		private void Awake()
		{
			mLayout = GetComponent<RollingInventoryLayout>();
		}

		public override void RemoveItem(Guid itemId)
		{
			var staticSlotIndex = mStaticSlotItemMapping.First(kvp => kvp.Value.ItemId.Equals(itemId)).Key;
			mStaticSlotItemMapping.Remove(staticSlotIndex);

			var dynamicSlotIndex = mLayout.CalculateDisplayIndexFor(staticSlotIndex);
			mAvailableUiSlots[staticSlotIndex].color = COLOR_TRANSPARENT;
			mAvailableUiSlots[staticSlotIndex].sprite = null;
			Debug.Log($"Removing slot no {staticSlotIndex} from position {dynamicSlotIndex}");
		}
	}
}
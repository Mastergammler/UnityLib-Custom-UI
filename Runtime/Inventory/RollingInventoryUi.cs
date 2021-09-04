using System;
using System.Collections;
using System.Collections.Generic;
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
			var slotIndex = mSlotItemMapping.First(kvp => kvp.Value.ItemId.Equals(itemId)).Key;
			var adjustedIndex = mLayout.CalculateDisplayIndexFor(slotIndex);
			mSlotItemMapping.Remove(adjustedIndex);
			mAvailableUiSlots[adjustedIndex].color = COLOR_TRANSPARENT;
			mAvailableUiSlots[adjustedIndex].sprite = null;
		}
	}
}
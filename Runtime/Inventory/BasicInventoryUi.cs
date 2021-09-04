using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace MgSq.UI.Inventory
{
	public class BasicInventoryUi : MonoBehaviour, IInventoryUi
	{
		/// <inheritdoc/>
		public event Action<Guid> OnItemSelected;

		//#################
		//##  CONSTANTS  ##
		//#################

		public static readonly string IMAGE_SLOT_OBJECT_NAME = "Item";
		public static readonly Color COLOR_TRANSPARENT = new Color(0, 0, 0, 0);
		public static readonly Color COLOR_WHITE = new Color(1, 1, 1, 1);

		//###############
		//##  MEMBERS  ##
		//###############

		protected Image[] mAvailableUiSlots;
		private int mUiSlotNumber;
		protected IDictionary<int, ItemView> mSlotItemMapping = new Dictionary<int, ItemView>();
		private Func<Transform, int, Image> getItemImageElement = (t, i) => t.GetChild(i).Find(IMAGE_SLOT_OBJECT_NAME).GetComponentInChildren<Image>();

		//################
		//##    MONO    ##
		//################

		void Start()
		{
			mUiSlotNumber = transform.childCount;
			mAvailableUiSlots = new Image[mUiSlotNumber];

			initalizeUi();
		}

		private void initalizeUi()
		{
			for (int i = 0; i < mUiSlotNumber; i++)
			{
				mAvailableUiSlots[i] = getItemImageElement(transform, i);
				mAvailableUiSlots[i].color = COLOR_TRANSPARENT;

				var curChild = transform.GetChild(i);
				curChild.GetComponent<Button>().onClick.AddListener(() => initButtonListener(curChild));
			}
		}

		protected virtual void initButtonListener(Transform buttonTransform)
		{
			int index = buttonTransform.GetSiblingIndex();
			if (mSlotItemMapping.TryGetValue(index, out ItemView view))
			{
				OnItemSelected?.Invoke(view.ItemId);
			}
		}

		//######################
		//##  I INVENTORY UI  ##
		//######################

		/// <inheritdoc/>
		public virtual void AddItem(ItemView item)
		{
			int foundSlot = findNextAvailableIndex();
			if (foundSlot != -1)
			{
				mSlotItemMapping.Add(foundSlot, item);
				mAvailableUiSlots[foundSlot].color = COLOR_WHITE;
				mAvailableUiSlots[foundSlot].sprite = item.Image;
			}
			else
			{
				Debug.LogError("The UI ran out of Item slots to display the collected item!!!");
			}
		}

		/// <inheritdoc/>
		public virtual void RemoveItem(Guid itemId)
		{
			var slotIndex = mSlotItemMapping.First(kvp => kvp.Value.ItemId.Equals(itemId)).Key;
			mSlotItemMapping.Remove(slotIndex);
			mAvailableUiSlots[slotIndex].color = COLOR_TRANSPARENT;
			mAvailableUiSlots[slotIndex].sprite = null;
		}

		//#################
		//##  AUXILIARY  ##
		//#################

		protected virtual int findNextAvailableIndex()
		{
			for (int i = 0; i < mUiSlotNumber; i++)
			{
				if (mAvailableUiSlots[i].color.Equals(COLOR_TRANSPARENT)) return i;
			}
			return -1;
		}
	}
}
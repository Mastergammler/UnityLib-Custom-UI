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
		/// <summary>
		/// Maps the view items to the slot index at initialization 
		/// </summary>
		/// <typeparam name="int">Slot index at initialization</typeparam>
		/// <typeparam name="ItemView">Item view that holds the display properties</typeparam>
		/// <returns></returns>
		protected IDictionary<int, ItemView> mStaticSlotItemMapping = new Dictionary<int, ItemView>();
		private Func<Transform, int, Image> getItemImageElement = (t, i) => t.GetChild(i).Find(IMAGE_SLOT_OBJECT_NAME)
																						 .GetComponentInChildren<Image>();

		//################
		//##    MONO    ##
		//################



		private void Awake()
		{
			// All childs have to be active, that the ElementIndexer script gets executed
			for (int i = 0; i < transform.childCount; i++)
			{
				transform.GetChild(i).gameObject.SetActive(true);
			}
		}

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

		private void initButtonListener(Transform buttonTransform)
		{
			int index = buttonTransform.GetComponent<ElementIndexer>().StaticIndex;
			if (mStaticSlotItemMapping.TryGetValue(index, out ItemView view))
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
			int freeStaticSlotIndex = findNextAvailableStaticIndex();
			if (freeStaticSlotIndex != -1)
			{
				mStaticSlotItemMapping.Add(freeStaticSlotIndex, item);
				mAvailableUiSlots[freeStaticSlotIndex].color = COLOR_WHITE;
				mAvailableUiSlots[freeStaticSlotIndex].sprite = item.Image;
			}
			else
			{
				Debug.LogError("The UI ran out of Item slots to display the collected item!!!");
			}
		}

		/// <inheritdoc/>
		public virtual void RemoveItem(Guid itemId)
		{
			var staticSlotIndex = mStaticSlotItemMapping.First(kvp => kvp.Value.ItemId.Equals(itemId)).Key;
			mStaticSlotItemMapping.Remove(staticSlotIndex);
			mAvailableUiSlots[staticSlotIndex].color = COLOR_TRANSPARENT;
			mAvailableUiSlots[staticSlotIndex].sprite = null;
		}

		//#################
		//##  AUXILIARY  ##
		//#################

		protected virtual int findNextAvailableStaticIndex()
		{
			for (int staticIndex = 0; staticIndex < mUiSlotNumber; staticIndex++)
			{
				if (mAvailableUiSlots[staticIndex].color.Equals(COLOR_TRANSPARENT)) return staticIndex;
			}
			return -1;
		}
	}
}
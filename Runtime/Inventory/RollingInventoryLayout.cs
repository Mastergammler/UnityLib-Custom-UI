using System;
using UnityEngine;
using UnityEngine.UI;

namespace MgSq.UI.Inventory
{
	/// <summary>
	/// Animation and layout handling for the rolling inventory ui. 
	/// It handles the scaling of the elements, repositoning and hiding of elements 
	/// As well as the item tracking of the active items. 
	/// </summary>
	public class RollingInventoryLayout : LayoutGroup
	{

		//##################
		//##    EDITOR    ##
		//##################

		#region Editor Values

		[Tooltip("Spacing between the button elements")]
		public float YSpacing = 30;

		[Tooltip("Numebr of inventory slots that are visible")]
		public int VisibleSlotNumber = 6;

		[Tooltip("Cell size that has been calculated by the component. It is not changable")]
		public Vector2 CellSize;

		[Tooltip("Modification factor to speed up or slow down the ui animation.")]
		public float AnimationSpeedMultiplicator = 1f;

		[Tooltip("Click this to update the Layout. It can not be automatically updated, because of animations that are run at runtime")]
		public bool ExecuteUpdate;

		#endregion

		//###############
		//##  MEMBERS  ##
		//###############

		private int mSlotOffset = 0;
		private bool mInAnimation = false;

		//################
		//##    MONO    ##
		//################

		private new void Start()
		{
			calculateLayoutNow();
			scaleSecondItem();
		}

		//#################
		//##  INTERFACE  ##
		//#################

		[Obsolete("Not needed anymore, because everything is based on the start index now.")]
		public int CalculateDisplayIndexFor(int originalIndex)
		{
			var displayIndex = (originalIndex + mSlotOffset) % transform.childCount;
			return displayIndex;
		}

		public void OnForward() => moveItems(true);
		public void OnBackward() => moveItems(false);

		//####################
		//##  LAYOUT GROUP  ##
		//####################

		public override void CalculateLayoutInputHorizontal()
		{
			if (ExecuteUpdate)
			{
				ExecuteUpdate = false;
				calculateLayoutNow();
			}
		}

		private void calculateLayoutNow()
		{
			base.CalculateLayoutInputHorizontal();

			deactivateHiddenSlots();
			calculateCellSize();
			setElementPositions();
			// scaleSecondItem();
		}

		public override void CalculateLayoutInputVertical() { }
		public override void SetLayoutHorizontal() { }
		public override void SetLayoutVertical() { }

		//#################
		//##  AUXILIARY  ##
		//#################

		#region Auxiliary Methods

		private void calculateCellSize()
		{
			float parentWidth = rectTransform.rect.width;
			float parentHeight = rectTransform.rect.height;

			CellSize.x = parentWidth - padding.left - padding.right;
			CellSize.y = (parentHeight - (VisibleSlotNumber - 1) * YSpacing - padding.top - padding.bottom)
							/ VisibleSlotNumber;
		}

		private void setElementPositions()
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				var rectItem = transform.GetChild(i).GetComponent<RectTransform>();
				var posX = padding.left;
				var posY = padding.top + (CellSize.y + YSpacing) * i;

				SetChildAlongAxis(rectItem, 0, posX, CellSize.x);
				SetChildAlongAxis(rectItem, 1, posY, CellSize.y);
			}
		}

		private float calculateAnimationTime(float baseTime) => baseTime * (1 / AnimationSpeedMultiplicator);

		private void scaleSecondItem()
		{
			LeanTween.scale(rectChildren[1].gameObject, new Vector3(1.5f, 1.5f, 1f), .2f);
		}

		private void moveToNextItem(float offset, RectTransform objectToDisappear, Action<RectTransform> adjustHierarchy)
		{

			for (int i = 0; i < transform.childCount; i++)
			{
				var curItem = transform.GetChild(i);
				LeanTween.moveLocalY(curItem.gameObject, curItem.localPosition.y + offset, calculateAnimationTime(1f));
			}


			LeanTween.scale(rectChildren[1].gameObject, new Vector3(1f, 1f, 1f), calculateAnimationTime(.5f));
			int slotOffset = offset > 0 ? 1 : -1;
			LeanTween.scale(rectChildren[1 + slotOffset].gameObject, new Vector3(1.5f, 1.5f, 1f), calculateAnimationTime(1f));
			LeanTween.scale(objectToDisappear.gameObject, new Vector3(.1f, .1f, 1f), calculateAnimationTime(.5f))
					 .setOnComplete(() =>
					 {
						 adjustHierarchy(objectToDisappear);
						 deactivateHiddenSlots();
					 });
			LeanTween.delayedCall(calculateAnimationTime(1.1f), () =>
			{
				calculateLayoutNow();
				mInAnimation = false;
			});
			//  .setOnComplete(() => mInAnimation = false);
		}

		private void moveItems(bool upward = true)
		{
			if (mInAnimation) return;
			mInAnimation = true;

			float offset;
			RectTransform scaleToDissapear;
			Action<RectTransform> action;

			if (upward)
			{
				offset = CellSize.y + YSpacing;
				scaleToDissapear = rectChildren[0];
				action = rect => rect.SetAsLastSibling();
			}
			else
			{
				offset = -(CellSize.y + YSpacing);
				scaleToDissapear = rectChildren[rectChildren.Count - 1];
				action = rect => rect.SetAsFirstSibling();
			}

			moveToNextItem(offset, scaleToDissapear, action);
			adjustSlotOffset(upward);
		}

		private void deactivateHiddenSlots()
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				if (i < VisibleSlotNumber)
				{
					transform.GetChild(i).gameObject.SetActive(true);
					if (i != 1)
						LeanTween.scale(transform.GetChild(i).gameObject, new Vector3(1f, 1f, 1f), calculateAnimationTime(.4f));
				}
				else
				{
					transform.GetChild(i).gameObject.SetActive(false);
					transform.GetChild(i).localScale = new Vector3(.1f, .1f, 1f);
				}
			}
		}


		private void adjustSlotOffset(bool forward = true)
		{
			if (forward) mSlotOffset--;
			else mSlotOffset++;

			if (mSlotOffset >= transform.childCount) mSlotOffset = 0;
			else if (mSlotOffset < 0) mSlotOffset = transform.childCount - 1;
		}

		#endregion
	}
}
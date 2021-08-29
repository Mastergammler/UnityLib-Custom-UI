using System;
using UnityEngine;
using UnityEngine.UI;

namespace MgSq.UI.Inventory
{
	public class RollingInventoryLayout : LayoutGroup
	{
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

		private int mSlotOffset = 0;

		private void Start()
		{
			calculateLayoutNow();
		}

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
			scaleSecondItem();
		}

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

		private void moveToNextItem(float offset, RectTransform objectToDissapear, Action<RectTransform> adjustHierarchy)
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				var curItem = transform.GetChild(i);
				LeanTween.moveLocalY(curItem.gameObject, curItem.localPosition.y + offset, calculateAnimationTime(1f));
			}

			LeanTween.scale(rectChildren[1].gameObject, new Vector3(1f, 1f, 1f), calculateAnimationTime(.3f));
			LeanTween.scale(objectToDissapear.gameObject, new Vector3(.1f, .1f, 1f), calculateAnimationTime(.4f))
					.setOnComplete(() =>
					{
						adjustHierarchy(objectToDissapear);
						deactivateHiddenSlots();
					});
			LeanTween.delayedCall(calculateAnimationTime(1.1f), () => calculateLayoutNow());
		}

		private void moveItems(bool upward = true)
		{
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

		[Obsolete("Not needed anymore, because everything is based on the start index now.")]
		public int CalculateDisplayIndexFor(int originalIndex)
		{
			var displayIndex = (originalIndex + mSlotOffset) % transform.childCount;
			return displayIndex;
		}

		private void adjustSlotOffset(bool forward = true)
		{
			if (forward) mSlotOffset--;
			else mSlotOffset++;

			if (mSlotOffset >= transform.childCount) mSlotOffset = 0;
			else if (mSlotOffset < 0) mSlotOffset = transform.childCount - 1;
		}

		public override void CalculateLayoutInputVertical() { }
		public override void SetLayoutHorizontal() { }
		public override void SetLayoutVertical() { }


		public void OnForward()
		{
			moveItems(true);
			adjustSlotOffset();
		}

		public void OnBackward()
		{
			moveItems(false);
			adjustSlotOffset(false);
		}
	}
}
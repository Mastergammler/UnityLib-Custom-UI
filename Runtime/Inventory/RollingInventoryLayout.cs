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

		[Tooltip("Click this to update the Layout. It can not be automatically updated, because of animations that are run at runtime")]
		public bool ExecuteUpdate;

		private int mSlotOffset = 0;

		private void Start()
		{
			CalculateLayoutNow();
		}

		public override void CalculateLayoutInputHorizontal()
		{
			if (ExecuteUpdate)
			{
				ExecuteUpdate = false;
				CalculateLayoutNow();
			}
		}

		public void CalculateLayoutNow()
		{
			base.CalculateLayoutInputHorizontal();

			DeactivateHiddenSlots();
			CalculateCellSize();
			SetElementPositions();
			ScaleSecondItem();
		}

		private void CalculateCellSize()
		{
			float parentWidth = rectTransform.rect.width;
			float parentHeight = rectTransform.rect.height;

			CellSize.x = parentWidth - padding.left - padding.right;
			CellSize.y = (parentHeight - (VisibleSlotNumber - 1) * YSpacing - padding.top - padding.bottom)
							/ VisibleSlotNumber;
		}

		private void SetElementPositions()
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

		private void ScaleSecondItem()
		{
			LeanTween.scale(rectChildren[1].gameObject, new Vector3(1.5f, 1.5f, 1f), .2f);
		}

		private void MoveToNextItem()
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				var curItem = transform.GetChild(i);
				LeanTween.moveLocalY(curItem.gameObject, curItem.localPosition.y + CellSize.y + YSpacing, 1f);
			}

			LeanTween.scale(rectChildren[1].gameObject, new Vector3(1f, 1f, 1f), .3f);
			LeanTween.scale(rectChildren[0].gameObject, new Vector3(.1f, .1f, 1f), .4f)
					.setOnComplete(() =>
					{
						rectChildren[0].SetAsLastSibling();
						DeactivateHiddenSlots();
					});
			LeanTween.delayedCall(1.1f, () => CalculateLayoutNow());
		}

		private void DeactivateHiddenSlots()
		{
			for (int i = 0; i < transform.childCount; i++)
			{
				if (i < VisibleSlotNumber)
				{
					transform.GetChild(i).gameObject.SetActive(true);
					if (i != 1)
						LeanTween.scale(transform.GetChild(i).gameObject, new Vector3(1f, 1f, 1f), .4f);
				}
				else
				{
					transform.GetChild(i).gameObject.SetActive(false);
					transform.GetChild(i).localScale = new Vector3(.1f, .1f, 1f);
				}
			}
		}

		public int CalculateDisplayIndexFor(int originalIndex)
		{
			var displayIndex = mSlotOffset + originalIndex;
			if (displayIndex >= transform.childCount)
				displayIndex -= transform.childCount;

			return displayIndex;
		}

		private void adjustSlotOffset(bool forward = true)
		{
			if (forward) mSlotOffset++;
			else mSlotOffset--;

			if (mSlotOffset >= transform.childCount) mSlotOffset = 0;
			else if (mSlotOffset < 0) mSlotOffset = transform.childCount - 1;
		}

		public override void CalculateLayoutInputVertical() { }
		public override void SetLayoutHorizontal() { }
		public override void SetLayoutVertical() { }


		public void OnForward()
		{
			MoveToNextItem();
			adjustSlotOffset();
		}

		public void OnBackward()
		{
			Debug.Log("Backward triggered");
			adjustSlotOffset(false);
		}
	}
}
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
				DeactivateHiddenSlots();
			}
		}

		public void CalculateLayoutNow()
		{
			base.CalculateLayoutInputHorizontal();

			CalculateCellSize();
			SetElementPositions();
			// DeactivateHiddenSlots();
			ScaleSecondItem();
			MoveToNextItem();
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
			for (int i = 0; i < rectChildren.Count; i++)
			{
				var rectItem = rectChildren[i];
				var posX = padding.left;
				var posY = padding.top + (CellSize.y + YSpacing) * i;

				SetChildAlongAxis(rectItem, 0, posX, CellSize.x);
				SetChildAlongAxis(rectItem, 1, posY, CellSize.y);
			}
		}

		private void ScaleSecondItem()
		{
			LeanTween.scale(rectChildren[1].gameObject, new Vector3(1.5f, 1.5f, 1f), .1f);
		}

		private void MoveToNextItem()
		{
			LeanTween.moveLocalY(transform.gameObject, CellSize.y + YSpacing, 2f);
			LeanTween.scale(rectChildren[1].gameObject, new Vector3(1f, 1f, 1f), .3f);
			LeanTween.scale(rectChildren[2].gameObject, new Vector3(1.5f, 1.5f, 1.5f), .8f);
		}

		private void DeactivateHiddenSlots()
		{
			for (int i = 0; i < rectChildren.Count; i++)
			{
				if (i >= VisibleSlotNumber) transform.GetChild(i).gameObject.SetActive(false);
				else transform.GetChild(i).gameObject.SetActive(true);
			}
		}

		public override void CalculateLayoutInputVertical() { }
		public override void SetLayoutHorizontal() { }
		public override void SetLayoutVertical() { }
	}
}
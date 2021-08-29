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

		public bool Update;


		private void Start()
		{
			CalculateLayoutInputHorizontal_New();
		}

		public override void CalculateLayoutInputHorizontal()
		{
			if (Update)
			{
				Update = false;
				CalculateLayoutInputHorizontal_New();
			}
		}

		public void CalculateLayoutInputHorizontal_New()
		{
			base.CalculateLayoutInputHorizontal();

			CalculateCellSize();
			SetElementPositions();
			DeactivateHiddenSlots();
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

		private void DeactivateHiddenSlots()
		{
			for (int i = 0; i < rectChildren.Count; i++)
			{
				if (i >= VisibleSlotNumber) rectChildren[i].gameObject.SetActive(false);
				else rectChildren[i].gameObject.SetActive(true);
			}
		}

		public override void CalculateLayoutInputVertical() { }
		public override void SetLayoutHorizontal() { }
		public override void SetLayoutVertical() { }
	}
}
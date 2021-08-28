using UnityEngine;
using UnityEngine.UI;

public class FlexibleLayoutGroup : LayoutGroup
{

	public enum FitType { UNIFORM, WIDTH, HEIGHT, FIXED_ROWS, FIXED_COLUMNS }
	public FitType FitBy;
	public int Rows;
	public int Columns;
	public Vector2 CellSize;
	public Vector2 Spacing;

	public bool FitX;
	public bool FitY;

	public override void CalculateLayoutInputHorizontal()
	{
		base.CalculateLayoutInputHorizontal();

		CalculateRowsAndColumns();
		CalculateCellSize();
		SetElementPositions();
	}


	private void CalculateRowsAndColumns()
	{
		if (FitBy == FitType.HEIGHT || FitBy == FitType.WIDTH || FitBy == FitType.UNIFORM)
		{
			FitX = FitY = true;

			float sqRt = Mathf.Sqrt(transform.childCount);
			Rows = Mathf.CeilToInt(sqRt);
			Columns = Mathf.CeilToInt(sqRt);
		}

		switch (FitBy)
		{
			case FitType.FIXED_COLUMNS:
			case FitType.WIDTH: Rows = Mathf.CeilToInt((float)transform.childCount / Columns); break;
			case FitType.FIXED_ROWS:
			case FitType.HEIGHT: Columns = Mathf.CeilToInt((float)transform.childCount / Rows); break;
		}
	}

	private void CalculateCellSize()
	{
		float parentWidth = rectTransform.rect.width;
		float parentHeight = rectTransform.rect.height;

		float cellWidth = (parentWidth - (Columns - 1) * Spacing.x - padding.left - padding.right)
							    / Columns;
		float cellHeight = (parentHeight - (Rows - 1) * Spacing.y - padding.top - padding.bottom)
							    / Rows;
		//Cell size is an attribute that unity searches for when building the ui
		CellSize.x = FitX ? cellWidth : CellSize.x;
		CellSize.y = FitY ? cellHeight : CellSize.y;
	}

	private void SetElementPositions()
	{
		for (int i = 0; i < rectChildren.Count; i++)
		{
			var item = rectChildren[i];
			var posX = padding.left + (CellSize.x + Spacing.x) * (i % Columns);
			var posY = padding.top + (CellSize.y + Spacing.y) * (i / Columns);

			SetChildAlongAxis(item, 0, posX, CellSize.x);
			SetChildAlongAxis(item, 1, posY, CellSize.y);
		}
	}

	public override void CalculateLayoutInputVertical() { }
	public override void SetLayoutHorizontal() { }
	public override void SetLayoutVertical() { }
}

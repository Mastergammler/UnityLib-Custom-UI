using System;
using UnityEngine;

namespace MgSq.UI.Inventory
{
	/// <summary>
	/// View class to represent a (inventory) item on the UI 
	/// </summary>
	public struct ItemView
	{
		public ItemView(Guid itemId, Sprite image)
		{
			ItemId = itemId;
			Image = image;
		}

		public Guid ItemId { get; }
		public Sprite Image { get; }

	}
}
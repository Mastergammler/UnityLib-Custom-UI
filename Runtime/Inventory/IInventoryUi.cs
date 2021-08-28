using System;

namespace MgSq.UI.Inventory
{
	/// <summary>
	/// Base events for updating and receiving updates from the inventory ui
	/// </summary>
	public interface IInventoryUi
	{
		/// <summary>
		/// Event triggered by Ui when a item gets selected 
		/// </summary>
		event Action<Guid> OnItemSelected;

		/// <summary>
		/// Will add a item view to the inventory ui 
		/// </summary>
		void AddItem(ItemView item);
		/// <summary>
		/// Removes the item with the specific id from the inventory ui 
		/// </summary>
		void RemoveItem(Guid itemId);
	}
}

using UnityEngine;

namespace MgSq.UI.Inventory
{
	/// <summary>
	/// Simple script to persist the STARTING Index of the element this button is on 
	/// Unsefull for registering click listeners, but having to know the original index. 
	/// Class is used by the <see cref="RollingInventoryUi"/>
	/// </summary>
	public class ElementIndexer : MonoBehaviour
	{
		private int mStartingIndex;
		public int StaticIndex => mStartingIndex;

		private void Awake()
		{
			mStartingIndex = transform.GetSiblingIndex();
		}
	}
}
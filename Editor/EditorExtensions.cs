using UnityEngine;
using UnityEditor;
using static MgSq.UI.Editor.MenuPaths;

namespace MgSq.UI.Editor
{
	[ExecuteInEditMode()]
	public class EditorExtensions : MonoBehaviour
	{
		[MenuItem(PATH_UI_MENU + NAME_LINEAR_PROGRESS_BAR)]
		public static void AddLinearProgressBar()
		{
			GameObject obj = Instantiate(Resources.Load<GameObject>("LinearProgressBar"));
			obj.transform.SetParent(Selection.activeGameObject.transform, false);
		}

		[MenuItem(PATH_UI_MENU + NAME_RADIAL_PROGRESS_BAR)]
		public static void AddRadialProgressBar()
		{
			GameObject obj = Instantiate(Resources.Load<GameObject>("RadialProgressBar"));
			obj.transform.SetParent(Selection.activeGameObject.transform, false);
		}

		[MenuItem(PATH_UI_MENU + NAME_INVENTORY_UI)]
		public static void AddInventoryUi()
		{
			GameObject obj = Instantiate(Resources.Load<GameObject>(MenuPaths.PREFAB_INVENTORY));
			obj.transform.SetParent(Selection.activeGameObject.transform, false);
		}

		[MenuItem(PATH_UI_MENU + NAME_ROLLING_INVENTORY_UI)]
		public static void AddRollingInventoryUi()
		{
			GameObject obj = Instantiate(Resources.Load<GameObject>(MenuPaths.PREFAB_ROLLING_INVENTORY));
			obj.transform.SetParent(Selection.activeGameObject.transform, false);
		}
	}
}

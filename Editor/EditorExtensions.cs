using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace MgSq.UI.Editor
{
    public class EditorExtensions
    {
        [MenuItem("GameObject/UI/Custom/Linear Progress Bar")]
        public static void AddLinearProgressBar()
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("LinearProgressBar"));
            obj.transform.SetParent(Selection.activeGameObject.transform,false);
        }

        [MenuItem("GameObject/UI/Custom/Radial Progress Bar")]
        public static void AddRadialProgressBar()
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("RadialProgressBar"));
            obj.transform.SetParent(Selection.activeGameObject.transform,false);
        }
    }
}

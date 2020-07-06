using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MgSq.UI
{
    public class EditorExtensions
    {
        #if UNITY_EDITOR

        [MenuItem("GameObject/UI/Linear Progress Bar")]
        public static void AddLinearProgressBar()
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("LinearProgressBar"));
            obj.transform.SetParent(Selection.activeGameObject.transform,false);
        }

        [MenuItem("GameObject/UI/Radial Progress Bar")]
        public static void AddRadialProgressBar()
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("RadialProgressBar"));
            obj.transform.SetParent(Selection.activeGameObject.transform,false);
        }

        #endif
    }
}

using System.ComponentModel.Design;
using System;
using System.Runtime.Versioning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace MgSq.UI
{
    [ExecuteInEditMode()]
    public class ProgressBar : MonoBehaviour
    {

        #if UNITY_EDITOR

        [MenuItem("GameObject/UI/Linear Progress Bar")]
        public static void AddLinearProgressBar()
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("UI/LinearProgressBar"));
            obj.transform.SetParent(Selection.activeGameObject.transform,false);
        }

        [MenuItem("GameObject/UI/Radial Progress Bar")]
        public static void AddRadialProgressBar()
        {
            GameObject obj = Instantiate(Resources.Load<GameObject>("UI/RadialProgressBar"));
            obj.transform.SetParent(Selection.activeGameObject.transform,false);
        }

        #endif


        [Tooltip("Weather or not there is a offest to be calculated based on a minimum value")]
        public bool DefaultBar = true;
        public int Minimum = 10;
        public int Maximum = 100;
        public int Current = 50;
        public Image Mask;

        public Color Color;
        public Image Fill;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if(DefaultBar)
            {
                calculateFill();
            }
            else
            {
                calculateFillThreshold();
            }
        }


        private void calculateFill()
        {
            Mask.fillAmount = (float)Current / Maximum;
            Fill.color = Color;

        }

        private void calculateFillThreshold()
        {
            var curOffest = Current - Minimum;
            var maxOffset = Maximum - Minimum;
            Mask.fillAmount = (float) curOffest / maxOffset;

            Fill.color = Color;
        }

    }
}

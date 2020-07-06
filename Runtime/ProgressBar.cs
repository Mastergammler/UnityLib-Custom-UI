using System.ComponentModel.Design;
using System;
using System.Runtime.Versioning;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace MgSq.UI
{
    [ExecuteInEditMode()]
    public class ProgressBar : MonoBehaviour
    {
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

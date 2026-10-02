using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace VTLTools.FixResolution
{
    [Serializable]
    public struct ScreenTemplate
    {
        #region Properties
        private const string FORMAT_NAME = "Screen {0}/{1}";
        [SerializeField]
        private float screenWidth,
            screenHeight;
        [SerializeField]
        private float canvasScalerMatch;

        public string Name => string.Format(FORMAT_NAME, screenWidth, screenHeight);
        public float ScreenWidth => screenWidth;
        public float ScreenHeight => screenHeight;
        public float ScreenAspect => screenWidth / screenHeight;
        public float MatchWidthOrHeight => canvasScalerMatch;
        #endregion Properties

        #region Construction
        public ScreenTemplate(float screenWidth, float screenHeight, float match)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;
            this.canvasScalerMatch = match;
        }
        #endregion Construction
    }
}
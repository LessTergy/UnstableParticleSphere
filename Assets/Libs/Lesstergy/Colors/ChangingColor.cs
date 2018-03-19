using Lesstergy.Math;
using Lesstergy.Time;
using UnityEngine;
using System;

namespace Lesstergy.Colors {

    [Serializable]
    public class ChangingColor {

        public Color value;
        public Color colorA;
        public Color colorB;

        [Space(10)]
        public RangeFloat timeRange;

        private TimeLerp timeLerp = new TimeLerp();

        public void Update() {
            value = Color.Lerp(colorA, colorB, timeLerp.currentValue);
            
            if (timeLerp.progress >= 1f) {
                SetNewColors();
                SetLerpTransition();
            }
        }

        private void SetNewColors() {
            colorA = colorB;
            colorB = ColorTool.GetRandom();
        }

        public void SetLerpTransition() {
            timeLerp.SetValues(0f, 1f, timeRange.Random());
        }
    }

}

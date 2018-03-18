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

        private TimeLerp deltaTimeLerp = new TimeLerp();

        public void Update() {
            if (deltaTimeLerp.progress == 0f) {
                SetNewColors();
            }
            value = Color.Lerp(colorA, colorB, deltaTimeLerp.value);

            deltaTimeLerp.Update();
            if (deltaTimeLerp.progress >= 1f) {
                SetDelta();
            }
        }

        private void SetNewColors() {
            colorA = colorB;
            colorB = ColorTool.GetRandom();
        }

        public void SetDelta() {
            deltaTimeLerp.SetValues(0f, 1f, timeRange.Random());
        }
    }

}

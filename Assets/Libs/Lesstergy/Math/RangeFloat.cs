using UnityEngine;
using System;

namespace Lesstergy.Math {

    [Serializable]
    public class RangeFloat {
        public float min;
        public float max;

        public RangeFloat(float min, float max) {
            this.min = min;
            this.max = max;
        }

        public float Random() {
            float value = UnityEngine.Random.Range(min, max);
            return value;
        }

        public float Clamp(float value) {
            return Mathf.Clamp(value, min, max);
        }
    }

}

using UnityEngine;

namespace Lesstergy.Math {

    public static class Mathl {

        public static float Map(float value, float fromA, float toA, float fromB, float toB) {
            float distanceFromBegin = Mathf.Abs(fromA - value);
            float totalA = Mathf.Abs(toA - fromA);

            float percent = distanceFromBegin / totalA;
            percent = Mathf.Clamp01(percent);

            float totalB = Mathf.Abs(fromB - toB);
            float valueToReturn = totalB * percent;

            if (fromB <= toB) {
                valueToReturn += fromB;
            } else {
                valueToReturn = fromB - valueToReturn;
            }

            return valueToReturn;
        }

        public static float MapOne(float value, float fromA, float toA) {
            return Map(value, fromA, toA, 0f, 1f);
        }

        public static float OneMinus(this float value) {
            return 1f - value;
        }
    }

}


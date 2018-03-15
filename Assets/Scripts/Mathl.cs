using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Mathl {

    public static float Map(float value, float fromA, float toA, float fromB, float toB) {
        float distanceFromBegin = Mathf.Abs(fromA - value);
        float totalA = Mathf.Abs(toA - fromA);

        float percent = distanceFromBegin / totalA;
        percent = Mathf.Clamp01(percent);

        float totalB = Mathf.Abs(fromB - toB);
        float valueToReturn = totalB * percent;
        valueToReturn += fromB;

        return valueToReturn;
    }
}

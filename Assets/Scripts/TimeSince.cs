using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSince {

    public float lastValue { get; private set; }
    
    public float delta {
        get {
            return orientationTime - lastValue;
        }
    }

    private float orientationTime {
        get {
            return Time.timeSinceLevelLoad;
        }
    }

    public void Fixate() {
        lastValue = orientationTime;
    }

    public void Increase() {
        lastValue += Time.deltaTime;
    }

    public void Reset() {
        lastValue = 0f;
    }
}

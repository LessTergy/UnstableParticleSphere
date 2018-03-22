using UnityEngine;
using System;

namespace Lesstergy.Time {

    //Lerp between two float values in a certain time

    [Serializable]
    public class TimeLerp {
        public float startValue;
        public float finishValue;
        public float timeLength;
        
        public float progress {
            get {
                return (UnityEngine.Time.timeSinceLevelLoad - lastTime) / timeLength;
            }
        }

        public bool isFinished {
            get {
                return progress >= 1f;
            }
        }

        private float lastTime;

        public void SetValues(float startValue, float finishValue, float timeLength) {
            this.startValue = startValue;
            this.finishValue = finishValue;
            this.timeLength = timeLength;

            lastTime = UnityEngine.Time.timeSinceLevelLoad;
        }

        public float currentValue {
            get {
                return Mathf.Lerp(startValue, finishValue, progress);
            }
        }
    }

}

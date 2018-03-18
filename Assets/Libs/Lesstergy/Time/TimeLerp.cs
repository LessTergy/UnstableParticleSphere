using UnityEngine;
using System;

namespace Lesstergy.Time {

    [Serializable]
    public class TimeLerp {
        public float startValue;
        public float finishValue;
        public float timeLength;
        
        public float progress { get; private set; }

        public void SetValues(float startValue, float finishValue, float time) {
            this.startValue = startValue;
            this.finishValue = finishValue;
            this.timeLength = time;
            progress = 0f;
        }

        public void Update() {
            progress += UnityEngine.Time.deltaTime / timeLength;
        }

        public float GetValueWithTime(float exactTime) {
            return Mathf.Lerp(startValue, finishValue, exactTime / timeLength);
        }

        public float value {
            get {
                return Mathf.Lerp(startValue, finishValue, progress);
            }
        }
    }

}

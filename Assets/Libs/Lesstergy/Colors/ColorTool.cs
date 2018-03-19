using UnityEngine;

namespace Lesstergy.Colors {

    public static class ColorTool {

        public static Color GetRandom() {
            return new Color(Random.value, Random.value, Random.value);
        }

    }
}
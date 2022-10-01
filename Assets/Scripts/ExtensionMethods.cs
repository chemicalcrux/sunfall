using UnityEngine;

public static class MathfExt {
    // -1 to 1, because that's more useful!
    public static float Logistic(float x, float k) {
        float denom = 1 + Mathf.Exp(-k * x);
        return -1 + 2 / denom;
    }
}
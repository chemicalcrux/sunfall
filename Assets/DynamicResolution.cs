using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class DynamicResolution : MonoBehaviour
{
    public float targetFPS = 60f;
    private float resolutionVelocity;
    public float currentFactor;

    private float runningAverage;
    // Start is called before the first frame update
    void Start()
    {
        DynamicResolutionHandler.SetDynamicResScaler(DoScaling, DynamicResScalePolicyType.ReturnsMinMaxLerpFactor);
        runningAverage = 1/60f;
        currentFactor = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float DoScaling()
    {
        if (PlayerPrefs.GetInt("Dynamic Resolution") == 0)
            return 1f;

        runningAverage *= 0.99f;
        runningAverage += 0.01f * Time.deltaTime;

        Debug.Log(Time.deltaTime);
        float target = runningAverage < 1 / targetFPS ? 1f : 0.25f;
        currentFactor = Mathf.SmoothDamp(currentFactor, target, ref resolutionVelocity, 5f);
        return currentFactor;
    }

}

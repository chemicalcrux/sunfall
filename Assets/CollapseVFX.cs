using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CollapseVFX : MonoBehaviour
{
    public GameStateHolder state;
    public Volume volume;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float t = state.pivot.collapseTimer;

        float t1 = Mathf.Clamp01(Mathf.InverseLerp(3, 0, t));
        float t2 = Mathf.Clamp01(Mathf.InverseLerp(9, 10, t));

        t = Mathf.Max(t1, t2);

        volume.weight = t;

        if (state.state != GameState.Playing)
            volume.weight = 0;
    }
}

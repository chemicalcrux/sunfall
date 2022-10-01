using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class KilledVFX : MonoBehaviour
{
    public GameStateHolder state;
    public Volume volume;
    private float velocity;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float target = state.state == GameState.Dead ? 1f : 0f;
        float rate = target == 1f ? 2.0f : 0.3f;
        volume.weight = Mathf.SmoothDamp(volume.weight, target, ref velocity, rate);

    }
}

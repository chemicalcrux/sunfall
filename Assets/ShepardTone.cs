using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShepardTone : MonoBehaviour
{
    public GameStateHolder state;
    public AnimationCurve lowCurve;
    public AnimationCurve highCurve;
    public AudioSource low;
    public AudioSource high;

    private float volumeMod;
    private float volumeVelocity;
    
    private float deathOffset;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (state.state == GameState.Dead) {
            deathOffset -= Time.deltaTime * 0.1f;
        } else {
            deathOffset = 0f;
        }
        float target = state.state == GameState.Playing ? 1f : 0f;

        volumeMod = Mathf.SmoothDamp(volumeMod, target, ref volumeVelocity, 1.5f);
        float t = 1 - state.pivot.collapseTimer / 10;
        t += 0.5f;
        t %= 1f;
        low.volume = volumeMod * PlayerPrefs.GetFloat("Music Volume") * lowCurve.Evaluate(t);
        high.volume = volumeMod * PlayerPrefs.GetFloat("Music Volume") * highCurve.Evaluate(t);
        low.pitch = t + 1 + deathOffset;
        high.pitch = t + 2 + deathOffset;
    }
}

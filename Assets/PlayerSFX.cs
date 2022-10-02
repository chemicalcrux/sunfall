using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    private AudioSource source;
    public AudioClip slam;
    public AudioClip bonk;
    public AudioClip crash;
    public AudioClip gameOverSlowdown;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Slam()
    {
        source.spatialBlend = 1f;
        source.volume = 0.5f;
        source.PlayOneShot(slam);
    }

    public void Bonk()
    {
        source.spatialBlend = 1f;
        source.volume = 1f;
        source.PlayOneShot(bonk);
    }

    public void Crash()
    {
        source.spatialBlend = 1f;
        source.volume = 1f;
        source.PlayOneShot(crash);
    }

    public void GameOverSlowdown()
    {
        source.spatialBlend = 0f;
        source.volume = 1f;
        source.PlayOneShot(gameOverSlowdown);
    }
}

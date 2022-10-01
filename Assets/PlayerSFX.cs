using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSFX : MonoBehaviour
{
    private AudioSource source;
    public AudioClip slam;
    public AudioClip crash;
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
        source.PlayOneShot(slam);
    }

    public void Crash()
    {
        source.PlayOneShot(crash);
    }
}

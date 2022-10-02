using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bobber : MonoBehaviour
{
    private Vector3 start;
    public Vector3 lower = Vector3.up;
    public Vector3 upper = Vector3.up;
    public float period = 1f;
    private float offset;
    // Start is called before the first frame update
    void Start()
    {
        start = transform.localPosition;
        offset = UnityEngine.Random.Range(0, 2 * Mathf.PI);
    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = start;
        transform.localPosition += (upper - lower) * (0.5f + 0.5f * Mathf.Sin(offset + Time.time * Mathf.PI * 2 / period)) + lower;
    }
}

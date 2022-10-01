using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    public Vector3 axis = Vector3.up;
    public float turnsPerSecond = 0.1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(360 * turnsPerSecond * Time.deltaTime, axis);
    }
}

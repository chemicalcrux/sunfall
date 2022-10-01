using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PivotController : MonoBehaviour
{
    public float radius = 1000f;

    [Range(0f, 1000f)]
    public float linearSpeed = 10f;
    float AngularSpeed => -linearSpeed / Mathf.PI / 2 / radius;
    public Vector3 axis = new Vector3(1, 0, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(360f * Time.deltaTime * AngularSpeed, axis);
    }

    public void Attach(Transform targetTransform, float turnOffset)
    {
        targetTransform.SetParent(transform);
        
        // turnOffset = 0 would drop it right on top of the player!

        float angle = 90f - turnOffset * 360f;
        float y = radius * Mathf.Sin(Mathf.Deg2Rad * angle);
        float z = radius * Mathf.Cos(Mathf.Deg2Rad * angle);
        targetTransform.transform.position = new Vector3(0, y, z);
        targetTransform.transform.rotation = Quaternion.AngleAxis(-angle, axis);
    }
}

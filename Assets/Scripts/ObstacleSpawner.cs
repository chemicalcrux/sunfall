using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleSpawner : MonoBehaviour
{
    private PivotController pivot;

    public GameObject course;
    [Range (0f, 1f)]
    public float obstacleOffset = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        pivot = GetComponent<PivotController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Returns the amount of space taken up
    public float SpawnCourse(float offset)
    {
        var obj = Instantiate(course);

        float furthest = offset;

        for (int i = 0; i < obj.transform.childCount; i++) {
            furthest = Mathf.Max(offset + obj.transform.GetChild(i).transform.localPosition.z, furthest);
        }

        while (obj.transform.childCount > 0) {
            var child = obj.transform.GetChild(0);
            pivot.Attach(child, offset, child.transform.localPosition);
        }

        return furthest;
    }
}

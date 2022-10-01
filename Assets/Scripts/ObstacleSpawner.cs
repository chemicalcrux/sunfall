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
        InvokeRepeating(nameof(SpawnCourse), 5.0f, 5.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void SpawnCourse()
    {
        var obj = Instantiate(course);

        while (obj.transform.childCount > 0) {
            var child = obj.transform.GetChild(0);
            pivot.Attach(child, obstacleOffset, child.transform.localPosition);
        }

    }
}

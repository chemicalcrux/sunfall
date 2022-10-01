using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleSpawner : MonoBehaviour
{
    private PivotController pivot;

    public GameObject obstacle;
    [Range (0f, 1f)]
    public float obstacleOffset = 0.2f;
    // Start is called before the first frame update
    void Start()
    {
        pivot = GetComponent<PivotController>();
        InvokeRepeating(nameof(CUBE), 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void CUBE()
    {
        var obj = Instantiate(obstacle);

        pivot.Attach(obj.transform, obstacleOffset);
    }
}

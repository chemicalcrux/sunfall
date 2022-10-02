using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ObstacleSpawner : MonoBehaviour
{
    private PivotController pivot;

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
    public float SpawnCourse(CourseCollection courseSet, float offset)
    {
        int index = UnityEngine.Random.Range(0, courseSet.courses.Count);
        var course = courseSet.courses[index];
        int repetitions = UnityEngine.Random.Range(course.repetitions.x, course.repetitions.y + 1);
        for (int copy = 0; copy < repetitions; copy++) {
            var obj = Instantiate(course);
            obj.GetComponent<Course>().Prepare();

            offset += course.frontBuffer;

            float furthest = offset;

            for (int i = 0; i < obj.transform.childCount; i++) {
                furthest = Mathf.Max(offset + obj.transform.GetChild(i).transform.localPosition.z, furthest);
            }

            Vector3 horizOffset = UnityEngine.Random.Range(course.horizontalLimits.x, course.horizontalLimits.y) * Vector3.right;

            while (obj.transform.childCount > 0) {
                var child = obj.transform.GetChild(0);
                pivot.Attach(child, offset, child.transform.localPosition + horizOffset);
            }

            offset = furthest + course.backBuffer;
        }


        return offset;
    }
}

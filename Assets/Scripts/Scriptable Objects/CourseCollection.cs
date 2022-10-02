using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Course Collection", fileName = "NewCourseCollection")]
public class CourseCollection : ScriptableObject
{
    public string label;
    public List<Course> courses;
    public float linearSpeed;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Course : MonoBehaviour
{
    public float frontBuffer;
    public float backBuffer;
    public Vector2 horizontalLimits = Vector2.zero;
    public Vector2Int repetitions = Vector2Int.one;

    public List<CourseModifier> modifiers;

    // Call this after instantiating the course prefab
    public void Prepare()
    {
        foreach (CourseModifier modifier in modifiers) {
            modifier.Execute(transform);
        }
    }
}

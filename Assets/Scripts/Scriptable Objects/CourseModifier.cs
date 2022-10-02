using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CourseModifier : ScriptableObject
{
    public abstract void Execute(Transform root);
}

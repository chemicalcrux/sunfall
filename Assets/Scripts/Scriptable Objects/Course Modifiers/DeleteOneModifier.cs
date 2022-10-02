using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Modifiers/Delete One", fileName = "NewDeleteOneModifier")]
public class DeleteOneModifier : CourseModifier
{
    public string nameFilter;
    public override void Execute(Transform root) {
        List<Transform> targets = new();

        for (int i = 0; i < root.childCount; i++) {
            if (nameFilter != "") {
                if (root.GetChild(i).name.Contains(nameFilter)) {
                    targets.Add(root.GetChild(i));
                }
            } else {
                targets.Add(root.GetChild(i));
            }
        }

        int index = UnityEngine.Random.Range(0, targets.Count);
        Debug.Log(index);
        Destroy(targets[index].gameObject);
    }
}

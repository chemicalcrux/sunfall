using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Modifiers/Cluster", fileName = "NewClusterModifier")]
public class ClusterModifier : CourseModifier
{
    public string nameFilter;
    public Vector2Int count;
    public float radius;
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

        foreach (Transform target in targets) {
            int iterations = UnityEngine.Random.Range(count.x, count.y + 1);

            for (int i = 0; i < iterations; i++) {
                var copy = Instantiate(target.gameObject);
                Vector2 randomPos = UnityEngine.Random.insideUnitCircle * radius;
                copy.transform.position = target.position + new Vector3(randomPos.x, 0, randomPos.y);
                copy.transform.SetParent(target.parent, true);
            }
        }
    }
}

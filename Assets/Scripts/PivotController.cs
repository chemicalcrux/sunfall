using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PivotController : MonoBehaviour
{
    public float radius = 1000f;
    public Transform ringTransform;

    [Range(0f, 1000f)]
    public float linearSpeed = 10f;
    float AngularSpeed => -linearSpeed / Mathf.PI / 2 / radius;
    public Vector3 axis = new Vector3(1, 0, 0);
    
    private Transform holder;

    // Start is called before the first frame update
    void Start()
    {
        var holderObj = new GameObject("Holder");
        holder = holderObj.transform;
        holder.SetParent(transform);
        holder.position = Vector3.zero;
        holder.rotation = Quaternion.identity;
        holder.localScale = Vector3.one;
    }

    public void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 200, 100), "Collapse")) {
            Collapse();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(360f * Time.deltaTime * AngularSpeed, axis);
    }

    public void Collapse()
    {
        radius -= 300f;
        ringTransform.localScale = new Vector3(radius/6, radius, radius);
        
        for (int i = 0; i < holder.childCount; i++) {
            var child = holder.GetChild(i).gameObject;
            var flyAway = child.AddComponent<FlyAway>();
            flyAway.direction = (child.transform.position - transform.position).normalized;
            flyAway.lifetime = 3f;
            flyAway.acceleration = 200f;
        }

        holder.DetachChildren();
    }

    public void Attach(Transform targetTransform, float turnOffset, Vector3 position)
    {
        targetTransform.SetParent(holder);
        
        // turnOffset = 0 would drop it right on top of the player!

        float angle = 90f - turnOffset * 360f;

        // next up: angle from the object's Z position

        angle -= 360f * position.z / Mathf.PI / 2 / radius;

        float offsetY = radius * Mathf.Sin(Mathf.Deg2Rad * angle);
        float offsetZ = radius * Mathf.Cos(Mathf.Deg2Rad * angle);

        Vector3 offsetPos = new Vector3(0, offsetY, offsetZ);

        // next up, we figure out where to place it, given its requested position

        float posX = position.x;
        float posY = position.y;

        offsetPos += new Vector3(posX, 0, 0);
        offsetPos += new Vector3(0, posY * Mathf.Cos(Mathf.Deg2Rad * angle), posY * Mathf.Sin(Mathf.Deg2Rad * angle));

        targetTransform.transform.position = offsetPos;
        targetTransform.transform.rotation = Quaternion.AngleAxis(90f - angle, axis);
    }
}

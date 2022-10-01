using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cinemachine;
public class PivotController : MonoBehaviour
{
    public float radius = 1000f;
    public Transform ringTransform;

    [Range(0f, 1000f)]
    public float linearSpeed = 10f;
    float AngularSpeed => -linearSpeed / Mathf.PI / 2 / radius;
    public Vector3 axis = new Vector3(1, 0, 0);

    public float collapseTimer = 10f;

    private Transform holder;

    public CinemachineImpulseSource rumbleSource;

    public ObstacleSpawner obstacleSpawner;

    private Dictionary<GameObject, float> obstacleKillTimes = new();

    // Start is called before the first frame update
    void Start()
    {
        var holderObj = new GameObject("Holder");
        holder = holderObj.transform;
        holder.SetParent(transform);
        holder.position = Vector3.zero;
        holder.rotation = Quaternion.identity;
        holder.localScale = Vector3.one;

        InvokeRepeating(nameof(CollapseWarning), 0.5f, 0.5f);
        ringTransform.localScale = new Vector3(500, radius, radius);

        obstacleSpawner = GetComponent<ObstacleSpawner>();
    }

    public void OnGUI()
    {
        if (GUI.Button(new Rect(20, 20, 200, 100), "Collapse"))
        {
            Collapse();
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation *= Quaternion.AngleAxis(360f * Time.deltaTime * AngularSpeed, axis);

        collapseTimer -= Time.deltaTime;

        if (collapseTimer <= 3f)
        {
            CheckObstacles();
        }
        if (collapseTimer <= 0)
        {
            Collapse();
        }

    }

    void CollapseWarning()
    {
        if (collapseTimer < 3f)
        {
            float force = (3f - collapseTimer) * 10f;
            rumbleSource.GenerateImpulseWithForce(force);
        }
    }

    void CheckObstacles()
    {
        List<GameObject> objects = obstacleKillTimes.Keys.ToList();
        foreach (var obj in objects)
        {
            if (obstacleKillTimes[obj] >= collapseTimer)
            {
                KillObstacle(obj);
                obstacleKillTimes.Remove(obj);
            }

        }
    }

    void KillObstacle(GameObject obstacle)
    {
        var flyAway = obstacle.AddComponent<FlyAway>();
        flyAway.direction = (obstacle.transform.position - transform.position).normalized;
        flyAway.lifetime = 3f;
        flyAway.acceleration = 200f;
        obstacle.transform.parent = null;
    }

    public void Collapse()
    {
        radius -= 750f;
        ringTransform.localScale = new Vector3(500, radius, radius);

        for (int i = 0; i < holder.childCount; i++)
        {
            var child = holder.GetChild(i).gameObject;
            KillObstacle(child);
        }

        holder.DetachChildren();

        collapseTimer = 10f;

        float offset = 1000;
        offset = obstacleSpawner.SpawnCourse(offset);
        offset = obstacleSpawner.SpawnCourse(offset);
        offset = obstacleSpawner.SpawnCourse(offset);
        offset = obstacleSpawner.SpawnCourse(offset);
        offset = obstacleSpawner.SpawnCourse(offset);
        offset = obstacleSpawner.SpawnCourse(offset);
    }

    public void Attach(Transform targetTransform, float distance, Vector3 position)
    {
        targetTransform.SetParent(holder);

        float angle = 90f;

        // next up: angle from the object's Z position

        angle -= 360f * (distance + position.z) / Mathf.PI / 2 / radius;

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

        obstacleKillTimes[targetTransform.gameObject] = UnityEngine.Random.Range(1.5f, 3f);
    }
}

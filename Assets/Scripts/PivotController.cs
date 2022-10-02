using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cinemachine;
public class PivotController : MonoBehaviour
{
    public GameStateHolder state;
    public CourseIndicator courseIndicator;
    public float radius = 1000f;
    public Transform ringTransform;
    public Transform previewRingTransform;

    public AudioSource rumbleAudio;

    [Range(0f, 2000f)]
    public float linearSpeed = 10f;
    float AngularSpeed => -linearSpeed / Mathf.PI / 2 / radius;
    public Vector3 axis = new Vector3(1, 0, 0);

    public float collapseTimer = 10f;

    private Transform holder;

    public CinemachineImpulseSource rumbleSource;

    public ObstacleSpawner obstacleSpawner;

    private Dictionary<GameObject, float> obstacleKillTimes = new();

    public MeshRenderer cylinder;

    private Quaternion nextLevelRotationStart;

    public int cyclesLeft;
    private CourseCollection activeCollection;

    public List<CourseCollection> courseCollections;

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

        ConfigureRings();

        obstacleSpawner = GetComponent<ObstacleSpawner>();
    }

    public void SelectCourseSet()
    {
        int index = UnityEngine.Random.Range(0, courseCollections.Count);

        activeCollection = courseCollections[index];
        cyclesLeft = 1; // UnityEngine.Random.Range(2, 5);
        linearSpeed = activeCollection.linearSpeed;

        courseIndicator.FlashName(activeCollection.label);
    }

    public void StartGame()
    {
        ConfigureRings();
    }

    private void ConfigureRings()
    {
        ringTransform.localScale = new Vector3(500, radius, radius);
        previewRingTransform.localScale = new Vector3(500, radius - 750, radius - 750);
        nextLevelRotationStart = UnityEngine.Random.rotationUniform;
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
        if (state.state != GameState.Playing)
            return;

        transform.rotation *= Quaternion.AngleAxis(360f * Time.deltaTime * AngularSpeed, axis);
        collapseTimer -= Time.deltaTime;

        state.score += linearSpeed * Time.deltaTime;

        if (collapseTimer <= 3f)
        {
            CheckObstacles();
        }
        if (collapseTimer <= 0)
        {
            Collapse();
        }

        float time = state.pivot.collapseTimer;

        float t1 = Mathf.Clamp01(Mathf.InverseLerp(3, 0, time));
        float t2 = Mathf.Clamp01(Mathf.InverseLerp(9, 10, time));

        time = Mathf.Max(t1, t2);

        rumbleAudio.volume = time * 0.5f;
        cylinder.material.SetFloat("_Integrity", Mathf.Clamp01(collapseTimer / 2));

        float t = Mathf.Clamp01(Mathf.InverseLerp(0, 10, collapseTimer));
        t = Mathf.Pow(t, 2);
        previewRingTransform.rotation = Quaternion.Slerp(nextLevelRotationStart, transform.rotation, 1 - t);
    }

    void CollapseWarning()
    {
        if (collapseTimer < 2f)
        {
            float force = (2f - collapseTimer) * 10f;
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
            }
        }
    }

    void KillObstacle(GameObject obstacle)
    {
        obstacleKillTimes.Remove(obstacle);

        // no idea why a single obstacle is leaking...

        if (obstacle == null)
            return;
        var flyAway = obstacle.AddComponent<FlyAway>();
        flyAway.direction = (obstacle.transform.position - transform.position).normalized;
        flyAway.lifetime = 3f;
        flyAway.acceleration = 200f;
        obstacle.transform.parent = null;
        obstacle.GetComponent<Obstacle>().Kill();
    }

    public void DestroyAllObstacles()
    {
        while (holder.childCount > 0)
        {
            KillObstacle(holder.GetChild(0).gameObject);
        }
    }

    public void Collapse()
    {
        radius -= 750f;

        ConfigureRings();
        DestroyAllObstacles();
        PrepareObstacles();

        collapseTimer = 10f;
    }

    public void PrepareObstacles()
    {
        --cyclesLeft;

        if (cyclesLeft <= 0) {
            SelectCourseSet();
        }

        float offset = 1500;

        while (offset < 1500 + linearSpeed * 10)
            offset = obstacleSpawner.SpawnCourse(activeCollection, offset);
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

        obstacleKillTimes[targetTransform.gameObject] = UnityEngine.Random.Range(1f, 2.5f);
    }
}

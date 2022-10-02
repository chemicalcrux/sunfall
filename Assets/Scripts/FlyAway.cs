using UnityEngine;

public class FlyAway : MonoBehaviour
{
    public Vector3 direction = Vector3.up;
    public float acceleration = 1f;
    public float lifetime = 3f;

    public float speed = 100f;

    private Quaternion rotationSpeed = Quaternion.identity;
    private new MeshRenderer renderer;

    void Start()
    {
        renderer = GetComponentInChildren<MeshRenderer>();

        Bobber bobber = GetComponentInChildren<Bobber>();

        if (bobber != null) {
            Destroy(bobber);
        }

        Spinner spinner = GetComponentInChildren<Spinner>();

        if (spinner != null) {
            Destroy(spinner);
        }
    }
    void Update()
    {
        speed += acceleration * Time.deltaTime;
        transform.position += Time.deltaTime * direction * speed;

        lifetime -= Time.deltaTime;

        rotationSpeed *= Quaternion.Slerp(Quaternion.identity, UnityEngine.Random.rotationUniform, 0.1f * Time.deltaTime);
        transform.rotation *= rotationSpeed;

        renderer.material.SetFloat("_Integrity", lifetime / 3);

        if (lifetime <= 0) {
            Destroy(gameObject);
        }

    }
}
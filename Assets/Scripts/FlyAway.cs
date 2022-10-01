using UnityEngine;

public class FlyAway : MonoBehaviour
{
    public Vector3 direction = Vector3.up;
    public float acceleration = 1f;
    public float lifetime = 3f;

    private float speed = 100f;

    private Quaternion rotationSpeed = Quaternion.identity;

    void Update()
    {
        speed += acceleration * Time.deltaTime;
        transform.position += Time.deltaTime * direction * speed;

        lifetime -= Time.deltaTime;

        rotationSpeed *= Quaternion.Slerp(Quaternion.identity, UnityEngine.Random.rotationUniform, 0.1f * Time.deltaTime);
        transform.rotation *= rotationSpeed;

        if (lifetime <= 0) {
            Destroy(gameObject);
        }

    }
}
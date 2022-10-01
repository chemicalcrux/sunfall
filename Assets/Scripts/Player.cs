using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    public float accel = 0.5f;
    public float decel = 1.5f;
    
    private Vector3 velocity;
    private float horizInput = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        Vector3 target = speed * Vector3.right * horizInput;
        float rate = 0f;
        // slowing down?
        if (target.x * velocity.x <= 0) {
            rate = decel;
        } else {
            rate = accel;
        }
        
        velocity = Vector3.MoveTowards(velocity, target, rate);

        transform.position += velocity;

        float tilt = -45f * MathfExt.Logistic(velocity.x / speed, 1f);

        transform.rotation = Quaternion.AngleAxis(tilt, Vector3.forward);
    }

    public void Horizontal(InputAction.CallbackContext context)
    {
        horizInput = context.ReadValue<float>();
    }
}

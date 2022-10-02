using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Cinemachine;
using UnityEngine.VFX;

public class Player : MonoBehaviour
{
    public enum CollisionKind {
        Deadly,
        Safe
    }

    public PlayerSFX sfx;
    public PivotController pivot;
    public float speed = 5f;
    public float accel = 0.5f;
    public float decel = 1.5f;
    
    private Vector3 velocity;
    private float horizInput = 0f;

    private float fallVelocity;
    public float fallSpeed = 50f;

    // just in case we move the pivot from the origin...
    public float Height => transform.position.y - pivot.transform.position.y;
    public float GoalHeight => pivot.radius + 25f;
    public bool Falling => Mathf.Abs(Height - GoalHeight) > 0.1f;
    public bool dead = false;

    public CinemachineImpulseSource normalImpulse;
    public CinemachineImpulseSource heavyImpulse;


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

        float tilt = -45f * MathfExt.Logistic(velocity.x / speed, 1f);

        transform.rotation = Quaternion.AngleAxis(tilt, Vector3.forward);

        if (Falling) {
            fallVelocity = Mathf.MoveTowards(fallVelocity, fallSpeed, Time.deltaTime * 25f);
        } else {
            if (fallVelocity > 0) {
                Impact(fallVelocity);
            }
            fallVelocity = 0f;
        }

        transform.position += velocity + Vector3.down * fallVelocity;

        if (Height < GoalHeight) {
            transform.position += Vector3.up * (GoalHeight - Height);
        }

    }

    public void Horizontal(InputAction.CallbackContext context)
    {
        horizInput = context.ReadValue<float>();
    }

    public void Impact(float impactSpeed)
    {
        sfx.Slam();        
        heavyImpulse.GenerateImpulseWithVelocity(Vector3.down * impactSpeed);
    }

    public void NearMiss(float distance)
    {
        heavyImpulse.GenerateImpulseWithForce(25f / distance);
    }

    public CollisionKind CheckCollision(Collider other) {
        
    }
    public void OnTriggerEnter(Collider other) {
        if (LayerMask.NameToLayer("Obstacle") != other.gameObject.layer)
            return;

        Obstacle obstacle = other.gameObject.GetComponentInParent<Obstacle>();
        
        if (obstacle == null)
            return;

        Kill();
    }

    public void Kill() {
        dead = true;
        sfx.Crash();
    }
}

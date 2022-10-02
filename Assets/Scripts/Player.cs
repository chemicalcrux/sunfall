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

    public GameStateHolder state;
    public PlayerSFX sfx;
    public VisualEffect leftSparks;
    public VisualEffect rightSparks;
    public VisualEffect impactFX;
    public AudioSource flyingSound;
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
        if (Height < GoalHeight) {
            transform.position += Vector3.up * (GoalHeight - Height);
        }
        flyingSound.volume = state.state == GameState.Playing ? 0.25f : 0f;
        if (state.state != GameState.Playing)
            return;
        Vector3 target = speed * Vector3.right * horizInput;
        float rate = 0f;
        // slowing down?
        if (target.x * velocity.x <= 0) {
            rate = decel;
        } else {
            rate = accel;
        }
        
        velocity = Vector3.MoveTowards(velocity, target, rate);

        float tilt = -45f * MathfExt.Logistic(velocity.x / speed, 2f);

        transform.rotation = Quaternion.AngleAxis(tilt, Vector3.forward);

        float droneTone = 2 + Mathf.Abs(tilt) / 45;
        flyingSound.pitch = droneTone;

        if (tilt >= 20f) {
            leftSparks.SendEvent("Start");
        } else {
            leftSparks.SendEvent("Stop");
        }
        if (tilt <= -20f) {
            rightSparks.SendEvent("Start");
        } else {
            rightSparks.SendEvent("Stop");
        }
        if (Falling) {
            fallVelocity = Mathf.MoveTowards(fallVelocity, fallSpeed, Time.fixedDeltaTime * 25f);
        } else {
            if (fallVelocity > 0) {
                Impact(Mathf.Clamp(fallVelocity, 0, 10));
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
        impactFX.SendEvent("Start");
    }

    public void NearMiss(float distance)
    {
        heavyImpulse.GenerateImpulseWithForce(25f / distance);
    }

    public void OnTriggerEnter(Collider other) {
        if (LayerMask.NameToLayer("Obstacle") != other.gameObject.layer)
            return;
            
        if (Physics.SphereCast(transform.position, 7.5f, transform.forward, out RaycastHit hit, 20f, LayerMask.GetMask("Obstacle"), QueryTriggerInteraction.Collide))
        {
            Debug.Log(hit);
        } 
        else
        {
            velocity.Scale(Vector3.left);
            sfx.Bonk();
            return;
        }
        Obstacle obstacle = other.gameObject.GetComponentInParent<Obstacle>();
        
        if (obstacle == null)
            return;

        Kill();
    }

    public void Kill() {
        dead = true;
        sfx.Crash();
        sfx.GameOverSlowdown();
    }
}

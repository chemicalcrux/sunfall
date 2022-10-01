using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using System.Linq;
public class Obstacle : MonoBehaviour {
    public List<Collider> colliders;
    public CinemachineImpulseSource impulseSource;
    public AudioSource whooshSource;

    private bool passed = false;
    private void Update() {
        if (passed)
            return;

        if (Camera.main.WorldToScreenPoint(transform.position).z > 0)
            return;

        passed = true;
        float distance = colliders.Select(collider => (collider.ClosestPoint(Camera.main.transform.position) - Camera.main.transform.position).magnitude).Min();

        impulseSource.GenerateImpulseWithForce(10f / distance);
        whooshSource.Play();
    }
}
using UnityEngine;
using Cinemachine;
using System.Collections.Generic;
using System.Linq;
public class Obstacle : MonoBehaviour {
    public List<Collider> safeColliders;
    public List<Collider> deadlyColliders;
    public CinemachineImpulseSource impulseSource;
    public AudioSource whooshSource;

    private bool passed = false;
    private void Update() {
        if (passed)
            return;

        if (Camera.main.WorldToScreenPoint(transform.position).z > 0)
            return;

        passed = true;
        float distance = safeColliders.Concat(deadlyColliders).Select(collider => (collider.ClosestPoint(Camera.main.transform.position) - Camera.main.transform.position).magnitude).Min();

        impulseSource.GenerateImpulseWithForce(10f / distance);
        whooshSource.Play();
    }

    public void Kill()
    {
        safeColliders.ForEach(collider => collider.enabled = false);
        deadlyColliders.ForEach(collider => collider.enabled = false);
    }
}
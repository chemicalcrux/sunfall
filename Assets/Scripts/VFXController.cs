using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Rendering;
public class VFXController : MonoBehaviour
{
    CinemachineIndependentImpulseListener listener;
    Volume volume;
    private float target;
    private float velocity;
    
    [CinemachineImpulseChannelProperty]
    public int impulseChannels;
    // Start is called before the first frame update
    void Start()
    {
        listener = GetComponent<CinemachineIndependentImpulseListener>();
        volume = GetComponent<Volume>();
    }

    // Update is called once per frame
    void Update()
    {
        CinemachineImpulseManager.Instance.GetImpulseAt(Vector3.zero, false, impulseChannels, out Vector3 offset, out Quaternion angle);
        float newTarget = Mathf.Log10(offset.magnitude * 100 + 1) / 2;

        target = Mathf.SmoothDamp(target, newTarget, ref velocity, 0.1f);
        volume.weight = target * SunfallMenu.VFXFactor();
    }
}

using UnityEngine;
using Cinemachine;
public class CameraDirector : MonoBehaviour {
    public Player player;
    public PivotController pivot;
    public CinemachineVirtualCamera followCam;
    public CinemachineVirtualCamera fallCam;
    public CinemachineVirtualCamera deadCam;

    public void Update()
    {
        fallCam.enabled = pivot.collapseTimer <= 1.5f;
        deadCam.enabled = player.dead;
    }
}
using UnityEngine;
using Cinemachine;
public class CameraDirector : MonoBehaviour {
    public Player player;
    
    public CinemachineVirtualCamera followCam;
    public CinemachineVirtualCamera fallCam;

    public void Update()
    {
        fallCam.enabled = player.Falling;
    }
}
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public bool RotateCamerabody_B = false;
    public CinemachineCamera CinemachineCamera;
    private void Update()
    {
        if (RotateCamerabody_B)
        {
            CinemachineCamera.GetComponent<CinemachineRotateWithFollowTarget>().enabled = true;
        }
        else
        {
            CinemachineCamera.GetComponent<CinemachineRotateWithFollowTarget>().enabled = false;
        }
    }
    private void Start()
    {

    }
}

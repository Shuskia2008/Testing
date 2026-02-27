using UnityEngine;

public class CameraController : MonoBehaviour
{
    public bool isParent;

    public bool RotateCamerabody_B = false;
    private float gravityAngle_G;

    private void Update()
    {
        if (isParent)
        {
            // Find the child, get their CameraController script, and apply it to the parent (only if this object is the parent).
            RotateCamerabody_B = transform.Find("Player").GetComponent<CameraController>().RotateCamerabody_B;
            gravityAngle_G = transform.Find("Player").GetComponent<Player_Movement_1_0_2>().GravityAngle_G;
        }
        if (RotateCamerabody_B == true)
        {
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, gravityAngle_G + 90f);
        }
        if (RotateCamerabody_B == false)
        {
            Camera.main.transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
    private void Start()
    {
        //RotateCamerabody_B = false;
    }
}

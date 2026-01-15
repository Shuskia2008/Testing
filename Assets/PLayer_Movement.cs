using UnityEngine;

public class PLayer_Movement : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float movespeed_L;
    public float movelength_L;
    public float movespeed_R;
    public float movelength_R;
    public float jumpspeed;
    public float jumpheight;
    public float Groundspeed;
    public float Gravityspeed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        //Jumping
        if (Input.GetKeyDown(KeyCode.UpArrow) == true || Input.GetKeyDown(KeyCode.W) == true)
        {
            myRigidbody.linearVelocityY = movespeed_L;
        }
        //Grounding
        if (Input.GetKeyDown(KeyCode.DownArrow) == true || Input.GetKeyDown(KeyCode.S) == true)
        {
            myRigidbody.linearVelocityY = -movespeed_L;
        }
        //Moving L
        if (Input.GetKey(KeyCode.LeftArrow) == true || Input.GetKeyDown(KeyCode.A) == true)
        {
            myRigidbody.linearVelocityX = -movespeed_L;
        }
        //Moving R
        if (Input.GetKey(KeyCode.RightArrow) == true || Input.GetKeyDown(KeyCode.D) == true)
        {
            myRigidbody.linearVelocityX = movespeed_L;
        }
        

    }
}

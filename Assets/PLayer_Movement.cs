using UnityEngine;

public class PLayer_Movement : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float movespeed_L;
    public float movelength_L;
    public float movespeed_R;
    public float movelength_R;
    public float jumpspeed_J;
    public float jumpheight_J;
    public float Groundspeed_G;
    public float Gravityspeed_G;
    public float variableYspeed_VVV;
    //dashing

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //myRigidbody.linearVelocityY = jumpspeed_J
    }

    // Update is called once per frame
    void Update()
    {
        //Jumping
        if ((Input.GetKeyDown(KeyCode.UpArrow) == true || Input.GetKeyDown(KeyCode.W) == true) && (GetComponent<Detection>().Detection_D == true))
        {
            variableYspeed_VVV = jumpheight_J;
            GetComponent<Detection>().Detection_D = false;
        }
        if (variableYspeed_VVV > 0)
        {
            variableYspeed_VVV = 0;
        }
        if (variableYspeed_VVV < 0)
        {
            variableYspeed_VVV += jumpspeed_J * Time.deltaTime * 0.5f;
            transform.position -= Vector3.up * variableYspeed_VVV * Time.deltaTime;
            variableYspeed_VVV += jumpspeed_J * Time.deltaTime * 0.5f;
        }
        //Grounding
        if (Input.GetKeyDown(KeyCode.DownArrow) == true || Input.GetKeyDown(KeyCode.S) == true)
        {
            myRigidbody.linearVelocityY = -Groundspeed_G;
        }
        //Moving L
        if ((Input.GetKey(KeyCode.LeftArrow) == true || Input.GetKeyDown(KeyCode.A) == true) && (GetComponent<Detection>().Detection_L == false))
        {
            myRigidbody.linearVelocityX = -movespeed_L;
        }
        //Moving R
        if ((Input.GetKey(KeyCode.RightArrow) == true || Input.GetKeyDown(KeyCode.D) == true) && (GetComponent<Detection>().Detection_R == false))
        {
            myRigidbody.linearVelocityX = movespeed_R;
        }
        

    }
}

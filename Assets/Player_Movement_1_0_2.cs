using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement_1_0_2 : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    //Moving
    //Left
    public float movespeed_L;
    public float movelength_L;
    public bool canMoving_L;
    //Right
    public float movespeed_R;
    public float movelength_R;
    public bool canMoving_R;
    //Jumping
    public float jumptime_J;
    public float jumpheight_J;
    public float jumptimeleft_J;
    public bool canJumping_J;
    //Grounding
    //Groundpound
    public float Groundspeed_G;
    public bool canGrounding_G;
    //Gravity
    public float Gravityspeed_G;
    //Dashing


    private float moveX;


    void Update()
    {
        //Gravity
        myRigidbody.linearVelocityY -= Gravityspeed_G * Time.deltaTime;
        Physics2D.gravity = new Vector2(0, 0);
        //Jumping
        if (jumptimeleft_J < 0)
        {
            jumptimeleft_J = 0;
        }
        if ((jumptime_J - jumptimeleft_J > 0.5) && ((GetComponent<Detection>().Detection_D == true) || (GetComponent<Detection>().Detection_U == true)))
        {
            jumptimeleft_J = 0;
        }
        if (jumptimeleft_J > 0)
        {
            jumptimeleft_J -= Time.deltaTime * 0.5f;
            transform.position += Vector3.up * 102 / 100 * jumptimeleft_J * Time.deltaTime * 2 * jumpheight_J / jumptime_J / jumptime_J;
            myRigidbody.linearVelocityY += Gravityspeed_G * Time.deltaTime;
            jumptimeleft_J -= Time.deltaTime * 0.5f;
        }
        
        //Grounding
        if ((Input.GetKeyDown(KeyCode.DownArrow) == true || Input.GetKeyDown(KeyCode.S) == true) && canGrounding_G == true)
        {
            myRigidbody.linearVelocityY = -Groundspeed_G;
        }
        //Moving L
        if ((Input.GetKey(KeyCode.LeftArrow) == true || Input.GetKey(KeyCode.A) == true) && (GetComponent<Detection>().Detection_L == false) && (canMoving_L == true))
        {
            myRigidbody.linearVelocityX = -movespeed_L;
        }
        //Moving R
        if ((Input.GetKey(KeyCode.RightArrow) == true || Input.GetKey(KeyCode.D) == true) && (GetComponent<Detection>().Detection_R == false) && (canMoving_R == true))
        {
            myRigidbody.linearVelocityX = movespeed_R;
        }
        //Moving M
        if ((Input.GetKey(KeyCode.LeftArrow) == false && Input.GetKey(KeyCode.A) == false) && (Input.GetKey(KeyCode.RightArrow) == false && Input.GetKey(KeyCode.D) == false) && (canMoving_L == true))
        {
            myRigidbody.linearVelocityX = 0;
        }

        myRigidbody.linearVelocityX = moveX;
    }
    public void Move(InputAction.CallbackContext ctx)
    {
        if ((GetComponent<Detection>().Detection_R == false) && (ctx.ReadValue<Vector2>().x > 0) && (canMoving_L == false) && (canMoving_R == false))
        {
            moveX = ctx.ReadValue<Vector2>().x * movespeed_L;
        }
        if ((GetComponent<Detection>().Detection_L == false) && (ctx.ReadValue<Vector2>().x < 0) && (canMoving_L == false) && (canMoving_R == false))
        {
            moveX = ctx.ReadValue<Vector2>().x * movespeed_R;
        }
        if ((ctx.ReadValue<Vector2>().x == 0) && (canMoving_L == false) && (canMoving_R == false))
        {
            moveX = ctx.ReadValue<Vector2>().x;
        }
    }
    public void Jump(InputAction.CallbackContext ctx)
    {
            if ((ctx.started) && (GetComponent<Detection>().Detection_D == true) && (canJumping_J == false))
            {
                jumptimeleft_J = jumptime_J;
                GetComponent<Detection>().Detection_D = false;
            }
            else if ((ctx.canceled) && (canJumping_J == false))
            {
                jumptimeleft_J = 0;
            }
    }
}

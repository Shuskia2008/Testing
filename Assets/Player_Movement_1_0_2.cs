using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement_1_0_2 : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    //Moving
    public float moveX;
    public float movecontroller;
    //-Left-
    public float movespeed_L;
    public float movelength_L;
    public bool canMoving_L;
    //-Right-
    public float movespeed_R;
    public float movelength_R;
    public bool canMoving_R;
    //Jumping
    public float jumptime_J;
    public float jumptimeleft_J;

    public float jumpheight_J;
    public bool canJumping_J;
    //Grounding
    //-Groundpound-
    public float Groundspeed_G;
    public bool canGrounding_G;
    //-Gravity-
    public float Gravityspeed_G;
    //Dashing
    public float GDx;
    public float GDy;



    void Update()
    {
        #region Moving_M

        if ((GetComponent<Detection>().Detection_R == false) && (movecontroller > 0) && (canMoving_L == false) && (canMoving_R == false))
        {
            moveX = movecontroller * movespeed_L;
        }
        else if ((GetComponent<Detection>().Detection_L == false) && (movecontroller < 0) && (canMoving_L == false) && (canMoving_R == false))
        {
            moveX = movecontroller * movespeed_R;
        }
        else if ((movecontroller == 0) && (canMoving_L == false) && (canMoving_R == false))
        {
            moveX = movecontroller;
        }
        else
        {
            moveX = 0;
        }
        myRigidbody.linearVelocityX = moveX;
        #endregion
        #region Jumping_J
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
        # endregion
        #region Grounding_G
        #region Gravity_G
        //Gravity
        //myRigidbody.linearVelocityX = GDx;
        //myRigidbody.linearVelocityY = GDy;
        myRigidbody.linearVelocityY -= Gravityspeed_G * Time.deltaTime;
        # endregion
        #region GroundPounding_G
        //Grounding
        if ((Input.GetKeyDown(KeyCode.DownArrow) == true || Input.GetKeyDown(KeyCode.S) == true) && canGrounding_G == true)
        {
            myRigidbody.linearVelocityY = -Groundspeed_G;
        }
        #endregion
        #endregion
    }
    #region Controller
    #region Moving Controller_M
    public void Move(InputAction.CallbackContext ctx)
    {
        movecontroller = ctx.ReadValue<Vector2>().x;
    }
    # endregion
    #region Jumping Controller_J
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
    #endregion
    #endregion
}

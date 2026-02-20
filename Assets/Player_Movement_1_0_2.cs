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


    #region Player
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
        //myRigidbody.linearVelocityY -= Gravityspeed_G * Time.deltaTime;
        Physics2D.gravity = new Vector2(0, 0);
        float angle = 90;
        float distance = 1;
        Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * distance;

        # endregion
        #region GroundPounding_G
        //Grounding
    #endregion
    #endregion
    }
    #endregion
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
    #region Grounding Controller_G
    #region Gravity Controller_G
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + (Vector3)myRigidbody.linearVelocity);


        float angle = 90 * Mathf.Deg2Rad;
        float distance = 1;
        Vector3 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + dir);
    }
    #endregion
    #region GroundPounding Controller_G
    public void GroundPounding(InputAction.CallbackContext ctx)
    {
        if (ctx.ReadValue<Vector2>().y == -1 && canGrounding_G == true)
        {
            myRigidbody.linearVelocityY = -Groundspeed_G;
        }
    }
    #endregion
    #endregion
    #endregion
}

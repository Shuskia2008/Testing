using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class Player_Movement_1_0_2 : MonoBehaviour
{
    #region Variables
    #region PlayerBody_B
    //Body
    public Rigidbody2D myRigidbody;
    #endregion
    #region Moving_M
    #region Moving_M
    //Moving
    public float moveX;
    public float movecontroller;
    #endregion
    #region Moving_L
    //-Left-
    public float movespeed_L;
    public float movelength_L;
    public bool canMoving_L;
    #endregion
    #region Moving_R
    //-Right-
    public float movespeed_R;
    public float movelength_R;
    public bool canMoving_R;
    #endregion
    #endregion
    #region Jumping_J
    //Jumping
    public float jumptime_J;
    public float jumptimeleft_J;
    public float jumpheight_J;
    public bool canJumping_J;
    private float JumpCodePlugIn;
    #endregion
    #region Grounding_G
    //Grounding
    #region Gravity_G
    //-Gravity-
    public float Gravityspeed_G;
    public float GravityAngle_G;
    public float GDx;
    public float GDy;
    #endregion
    #region GroundPounding_G
    //-Groundpound-
    public float Groundpoundingspeed_G;
    private bool currentlyGroundpounding_G;
    public bool canGroundounding_G;
    #endregion
    #endregion
    #region Dashing_D
    //Dashing
    #endregion
    #endregion
    #region Player
    void Update()
    {
            float angle = GravityAngle_G * Mathf.Deg2Rad;
            float distance = Gravityspeed_G;
            GDx = Mathf.Cos(angle);
            GDy = Mathf.Sin(angle);
        #region PlayerBody_B
        //Body
        transform.rotation = Quaternion.Euler(0, 0, GravityAngle_G + 90);
        #endregion
        #region Moving_M

        if ((GetComponent<Detection>().Detection_R == false) && (movecontroller > 0) && (canMoving_R == true))
        {
            myRigidbody.linearVelocityX = -GDy * movecontroller * movespeed_R;
            myRigidbody.linearVelocityY = GDx * movecontroller * movespeed_R;
        }
        else if ((GetComponent<Detection>().Detection_L == false) && (movecontroller < 0) && (canMoving_L == true))
        {
            myRigidbody.linearVelocityX = -GDy * movecontroller * movespeed_L;
            myRigidbody.linearVelocityY = GDx * movecontroller * movespeed_L;
        }
        else
        {
            myRigidbody.linearVelocityX = 0;
            myRigidbody.linearVelocityY = 0;
        }
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
            /*jumptimeleft_J -= Time.deltaTime * 0.5f;
            transform.position += Vector3.up * 102 / 100 * jumptimeleft_J * Time.deltaTime * 2 * jumpheight_J / jumptime_J / jumptime_J;
            myRigidbody.linearVelocityY += Gravityspeed_G * Time.deltaTime;
            jumptimeleft_J -= Time.deltaTime * 0.5f;*/
            JumpCodePlugIn = 102 / 100 * jumptimeleft_J * Time.deltaTime * 2 * jumpheight_J / jumptime_J / jumptime_J;
            jumptimeleft_J -= Time.deltaTime * 0.5f;
            transform.position += new Vector3(-GDx * JumpCodePlugIn, -GDy * JumpCodePlugIn);
            myRigidbody.linearVelocityY += Gravityspeed_G * Time.deltaTime;
            jumptimeleft_J -= Time.deltaTime * 0.5f;
        }
        # endregion
        #region Grounding_G
        //Grounding
        #region Gravity_G
        //Gravity
        Physics2D.gravity = new Vector2(GDx * distance, GDy * distance);
        //myRigidbody.linearVelocityX = GDx;
        //myRigidbody.linearVelocityY = GDy;
        //myRigidbody.linearVelocityY -= Gravityspeed_G * Time.deltaTime;
        //float angle = 90;
        //float distance = 1;
        //Vector2 dir = new Vector2(Mathf.Sin(angle), Mathf.Cos(angle)) * distance;
        #endregion
        #region GroundPounding_G
        //GroundPounding
        if (currentlyGroundpounding_G == true)
        {
            myRigidbody.linearVelocityX = GDx * Groundpoundingspeed_G;
            myRigidbody.linearVelocityY = GDy * Groundpoundingspeed_G;
        }
        if ((GetComponent<Detection>().Detection_D == true) && (currentlyGroundpounding_G == true))
        {
            currentlyGroundpounding_G = false;
        }
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
            if ((ctx.performed) && (GetComponent<Detection>().Detection_D == true) && (canJumping_J == true))
            {
                jumptimeleft_J = jumptime_J;
                GetComponent<Detection>().Detection_D = false;
            }
            else if ((ctx.canceled) && (canJumping_J == true))
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


        float angle = GravityAngle_G * Mathf.Deg2Rad;
        float distance = Gravityspeed_G;
        Vector3 dir = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;

        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position, transform.position + dir);
    }
    #endregion
    #region GroundPounding Controller_G
    public void GroundPounding(InputAction.CallbackContext ctx)
    {
        if (ctx.performed && canGroundounding_G == true && (GetComponent<Detection>().Detection_D == false))
        {
            currentlyGroundpounding_G = true;
        }
        if (ctx.canceled && canGroundounding_G == true && (GetComponent<Detection>().Detection_D == false))
        {
            currentlyGroundpounding_G = false;
        }
    }
    #endregion
    #endregion
    #endregion
}

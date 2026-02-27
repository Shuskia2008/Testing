using Unity.Cinemachine;
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
    public CinemachineCamera CinemachineCamera;
    public bool RotateCamerabody_B;
    #endregion
    #region Moving_M
    #region Moving_M
    //Moving
    public float moveX;
    public float movecontroller;
    #endregion
    #region Moving_L
    //-Left-
    public float movetime_L;
    public float movelength_L;
    public bool canMoving_L;
    #endregion
    #region Moving_R
    //-Right-
    public float movetime_R;
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
    public bool canGroundpounding_G;
    #endregion
    #endregion
    #region Dashing_D
    //Dashing
    #endregion
    #endregion
    #region Player
    void Update()
    {
        #region PlayerVariables
        //PlayerVariables
        float angle = GravityAngle_G * Mathf.Deg2Rad;
            GDx = Mathf.Cos(angle);
            GDy = Mathf.Sin(angle);
        #endregion
        #region PlayerBody_B
        //Body
        transform.rotation = Quaternion.Euler(0, 0, GravityAngle_G + 90);
        if (RotateCamerabody_B)
        {
            CinemachineCamera.GetComponent<CinemachineRotateWithFollowTarget>().enabled = true;
        }
        else
        {
            CinemachineCamera.GetComponent<CinemachineRotateWithFollowTarget>().enabled = false;
        }
        #endregion
        #region Moving_M
        //Moving
        if ((GetComponent<Detection>().Detection_R == false) && (movecontroller > 0) && (canMoving_R == true))
        {
            transform.position += new Vector3(-GDy * movecontroller * movelength_R / movetime_R * Time.deltaTime, GDx * movecontroller * movelength_R / movetime_R * Time.deltaTime);
        }
        else if ((GetComponent<Detection>().Detection_L == false) && (movecontroller < 0) && (canMoving_L == true))
        {
            transform.position += new Vector3(-GDy * movecontroller * movelength_L / movetime_L * Time.deltaTime, GDx * movecontroller * movelength_L / movetime_L * Time.deltaTime);
        }
        else
        {
            //myRigidbody.linearVelocityX = 0;
            //myRigidbody.linearVelocityY = 0;
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
            JumpCodePlugIn = 110 / 100 * jumptimeleft_J * Time.deltaTime * 2 * jumpheight_J / jumptime_J / jumptime_J;
            jumptimeleft_J -= Time.deltaTime * 0.5f;
            transform.position += new Vector3(-GDx * JumpCodePlugIn, -GDy * JumpCodePlugIn);
            myRigidbody.linearVelocityX = -GDx * Gravityspeed_G * Time.deltaTime;
            myRigidbody.linearVelocityY = -GDy * Gravityspeed_G * Time.deltaTime;
            jumptimeleft_J -= Time.deltaTime * 0.5f;
        }
        # endregion
        #region Grounding_G
        //Grounding
        #region Gravity_G
        //Gravity
        Physics2D.gravity = new Vector2(GDx * Gravityspeed_G, GDy * Gravityspeed_G);
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
        if (ctx.performed && canGroundpounding_G == true && (GetComponent<Detection>().Detection_D == false))
        {
            currentlyGroundpounding_G = true;
        }
        if (ctx.canceled && canGroundpounding_G == true && (GetComponent<Detection>().Detection_D == false))
        {
            currentlyGroundpounding_G = false;
        }
    }
    #endregion
    #endregion
    #endregion
}

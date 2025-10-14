using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    public float movespeed;
    public float moveset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        moveset = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) == true && moveset == 1)
        {
            myRigidbody.linearVelocityY = movespeed;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) == true && moveset == 1)
        {
            myRigidbody.linearVelocityX = -movespeed;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) == true && moveset == 1)
        {
            myRigidbody.linearVelocityX = movespeed;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) == true && moveset == 1)
        {
            myRigidbody.linearVelocityY = -movespeed;
        }





        if (Input.GetKeyDown(KeyCode.Space) == true && moveset == 1)
        {
            moveset = 2;
        }
        else if (Input.GetKeyDown(KeyCode.Space) == true && moveset == 2)
        {
            moveset = 1;
            Physics2D.gravity = new Vector2(0, -9.81f);
            myRigidbody.freezeRotation = true;
        }




        if (Input.GetKeyDown(KeyCode.UpArrow) == true && moveset == 2)
        {
            Physics2D.gravity = new Vector2(0, 15f);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) == true && moveset == 2)
        {
            Physics2D.gravity = new Vector2(-15f, 0);
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) == true && moveset == 2)
        {
            Physics2D.gravity = new Vector2(15f, 0);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) == true && moveset == 2)
        {
            Physics2D.gravity = new Vector2(0, -15f);
        }
    }
}

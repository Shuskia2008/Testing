using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D myRigidbody;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      if (Input.GetKeyDown(KeyCode.UpArrow) == true)
        {
            myRigidbody.linearVelocityY = 10;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) == true)
        {
            myRigidbody.linearVelocityX = -10;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow) == true)
        {
            myRigidbody.linearVelocityX = 10;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) == true)
        {
            myRigidbody.linearVelocityY = -10;
        }
    }
}

using UnityEngine;

public class Detection : MonoBehaviour
{
    public bool Detection_L = false;
    public bool Detection_R = false;
    public bool Detection_U = false;
    public bool Detection_D = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Tile") && gameObject.name == "Detect L")
        {
            Detection_L = true;
        }
        if (collision.gameObject.CompareTag("Tile") && gameObject.name == "Detect R")
        {
            Detection_R = true;
        }
        if (collision.gameObject.CompareTag("Tile") && gameObject.name == "Detect U")
        {
            Detection_U = true;
        }
        if (collision.gameObject.CompareTag("Tile") && gameObject.name == "Detect D")
        {
            Detection_D = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (gameObject.name == "Detect L")
        {
            Detection_L = false;
        }
        if (gameObject.name == "Detect R")
        {
            Detection_R = false;
        }
        if (gameObject.name == "Detect U")
        {
            Detection_U = false;
        }
        if (gameObject.name == "Detect D")
        {
            Detection_D = false;
        }
    }
}

using UnityEngine;
using UnityEngine.SceneManagement;

public class inventoryCodeButtons : MonoBehaviour
{
    public void EXIT()
    {
        //EXITs Inventory Menu
        SceneManager.LoadScene("Game");
    }
}

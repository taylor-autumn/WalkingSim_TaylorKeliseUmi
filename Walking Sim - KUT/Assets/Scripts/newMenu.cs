using UnityEngine;
using UnityEngine.SceneManagement;

public class newMenu : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene("main");
    }

    public void backToMenu()
    {
        SceneManager.LoadScene("menu");
    }

}

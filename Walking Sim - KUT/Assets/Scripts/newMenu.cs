using UnityEngine;
using UnityEngine.SceneManagement;

public class newMenu : MonoBehaviour
{
    public void playGame()
    {
        SceneManager.LoadScene("main");
    }

    public void quitGame()
    {
        Application.Quit();
        print("quitting");
    }

}

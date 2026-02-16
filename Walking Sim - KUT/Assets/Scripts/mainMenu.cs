using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class mainMenu : MonoBehaviour
{

    public TMP_Text aboutText;
    public TMP_Text bigTitleText;
    public TMP_Text controlText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public void startPlay()
    {
        SceneManager.LoadScene("main");
    }

    public void aboutButton()
    {
        print("Clicking the about button.");
        aboutText.text="about stuff";
        // aboutText.text.SetActive(true);
        // bigTitleText.text.SetActive(false);
        // controlText.text.
        //
        
    }

    public void controlButton()
    {
        print("Clicking the control button.");
        aboutText.text="control stuff";
        
        // controlText.gameObject.SetActive(true);
        // bigTitleText.gameObject.SetActive(false);
        //
        
    }

}

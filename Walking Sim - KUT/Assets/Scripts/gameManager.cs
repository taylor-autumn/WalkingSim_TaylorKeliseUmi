using UnityEngine;

public enum gameState { normalMode, interactMode}
public enum timeOfDay { beforeClass,inClass,lunch,gamesClub,evening,night }
public class gameManager : MonoBehaviour
{
    public gameState state;
    public timeOfDay gameLevel;
    private GameObject michelle;
    public interactCircle currentInteract;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state=gameState.normalMode;
        gameLevel=timeOfDay.beforeClass;
        michelle = GameObject.Find("Michelle");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            nextLevel();

        }

        if (gameLevel==timeOfDay.beforeClass)
        {
            michelle.SetActive(false);
        }
        else
        {
            michelle.SetActive(true);
        }

    }

    void nextLevel()
    {
        int levelCount=System.Enum.GetValues(typeof(timeOfDay)).Length;
        int nextLevel = ((int)gameLevel + 1) % levelCount;
        gameLevel=(timeOfDay)nextLevel;

        print("On Level " +  gameLevel);


    }
}

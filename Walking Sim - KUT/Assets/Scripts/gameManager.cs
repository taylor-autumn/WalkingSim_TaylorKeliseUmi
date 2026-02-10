using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum gameState { normalMode, interactMode}
public enum timeOfDay { beforeClass,inClass,lunch,gamesClub,evening,night }

public class gameManager : MonoBehaviour
{
    public gameState state;
    public timeOfDay gameLevel;
    public interactCircle currentInteract;
    public Animator theSun;
    GameObject characterParents;
    public int trackInteract = 0;

    [Header("objects and text")]
    private GameObject michelle;
    public GameObject modeBox;
    public GameObject nextLevelButton;
    public TMP_Text modeText;
    public TMP_Text trackingText;
    public TMP_Text maxInteractNumber;
    public GameObject pushButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state=gameState.normalMode;
        gameLevel=timeOfDay.beforeClass;
        michelle = GameObject.Find("Michelle");
        characterParents = GameObject.Find("characters");
        pushButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) && state == gameState.normalMode)
        {
            nextLevel();
        }

        if (state == gameState.normalMode)
        {
            modeBox.SetActive(true);
            nextLevelButton.SetActive(true);
        }
        else
        {
            modeBox.SetActive(false);
            nextLevelButton.SetActive(false);
        }

        if (gameLevel == timeOfDay.beforeClass)
        {
            michelle.SetActive(false);
        }
        else
        {
            michelle.SetActive(true);
        }

        //always changes the text based off the mode/gameLevel
        changeText();

        if (state == gameState.normalMode)
        {
            if (checkStatus(characterParents))
            {
                pushButton.SetActive(true);
            }
            else
            {
                pushButton.SetActive(false);
            }
        }

    }

    void nextLevel()
    {
        if (checkStatus(characterParents))
        {
            int levelCount = System.Enum.GetValues(typeof(timeOfDay)).Length;
            int nextLevel = ((int)gameLevel + 1) % levelCount;
            gameLevel = (timeOfDay)nextLevel;
            theSun.SetTrigger("change"); //changes the sun
            print("On Level " + gameLevel);
            resetCharacters(characterParents);
            //pushButton.SetActive(false);
            trackInteract = 0;
        }
        else
        {
            print("can't move on yet");
        }

    }

    public void changeText()
    {
        trackingText.text = trackInteract.ToString();

        if (gameLevel == timeOfDay.beforeClass)
        {
            maxInteractNumber.text = "8";
        }
        else
        {
            maxInteractNumber.text = "9";
        }

        if (gameLevel == timeOfDay.beforeClass)
        {
            modeText.text = "Current Time: 8:55 AM";
        }
        else if (gameLevel == timeOfDay.inClass)
        {
            modeText.text = "Current Time: 9:30 AM";
        }
        else if (gameLevel == timeOfDay.lunch)
        {
            modeText.text = "Current Time: 12:00PM";
        }
        else if (gameLevel == timeOfDay.gamesClub)
        {
            modeText.text = "Current Time: 4:00 PM";
        }
        else if (gameLevel == timeOfDay.evening)
        {
            modeText.text = "Current Time: 8:00 PM";
        }
        else
        {
            modeText.text = "Current Time: 3:00 AM";
        }
        
    }

    public bool checkStatus(GameObject parentCharacters)
    {
        foreach (Transform character in parentCharacters.transform)
        {
            unitInfo charUnit = character.GetComponent<unitInfo>();
            if (!character.gameObject.activeInHierarchy)
            {
                continue;
            }

            if (charUnit.firstInteraction)
                return false;
        }
        return true; // all active characters are done

    }

    public void resetCharacters(GameObject parentCharacters)
    {
        foreach (Transform character in parentCharacters.transform)
        {
            unitInfo charUnit = character.GetComponent<unitInfo>();
            charUnit.firstInteraction = true;
        }
    }

}

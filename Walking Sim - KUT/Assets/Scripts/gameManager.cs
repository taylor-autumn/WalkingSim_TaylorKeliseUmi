using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

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

        changeCharPositions(characterParents);
    }

    // Update is called once per frame
    void Update()
    {

        //cheat next level
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.P))
        {
            hackNextLevel();
            print("hacking next level");
        }



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

        if (checkStatus(characterParents) && state == gameState.normalMode)
        {
            pushButton.SetActive(true);
        }
        else
        {
            pushButton.SetActive(false);
        }

    }

    void nextLevel()
    {
        if (checkStatus(characterParents))
        {
            //changes the time of day
            int levelCount = System.Enum.GetValues(typeof(timeOfDay)).Length;
            int nextLevel = ((int)gameLevel + 1) % levelCount;
            gameLevel = (timeOfDay)nextLevel;

            //changes the sun
            theSun.SetTrigger("change"); //changes the sun
            print("On Level " + gameLevel);

            //resets the characters interact so you have to interact with them again
            resetCharacters(characterParents);
            trackInteract = 0;

            //moves the characters
            addPositionIndex(characterParents);
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
            //taylor.transform.position = respawn.transform.position;
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

    public void changeCharPositions(GameObject parentCharacters)
    {
        foreach (Transform character in parentCharacters.transform)
        {
            unitInfo charUnit = character.GetComponent<unitInfo>();
            if (charUnit == null) continue;

            List<GameObject> positionList = charUnit.charPositions;

            if (positionList.Count == 0) continue;

            charUnit.transform.position =
                positionList[charUnit.charPositionIndex].transform.position;
            charUnit.transform.rotation =
                positionList[charUnit.charPositionIndex].transform.rotation;
        }
    }

    public void addPositionIndex(GameObject parentCharacters)
    {
        foreach (Transform character in parentCharacters.transform)
        {
            unitInfo charUnit = character.GetComponent<unitInfo>();
            if (charUnit == null) continue;

            if (charUnit.charPositions.Count == 0) continue;

            charUnit.charPositionIndex =
                (charUnit.charPositionIndex + 1) % charUnit.charPositions.Count;
        }

        changeCharPositions(parentCharacters);
    }

    public void hackNextLevel()
    {
        //changes the time of day
        int levelCount = System.Enum.GetValues(typeof(timeOfDay)).Length;
        int nextLevel = ((int)gameLevel + 1) % levelCount;
        gameLevel = (timeOfDay)nextLevel;

        //changes the sun
        theSun.SetTrigger("change"); //changes the sun
        print("On Level " + gameLevel);

        //resets the characters interact so you have to interact with them again
        resetCharacters(characterParents);
        trackInteract = 0;

        //moves the characters
        addPositionIndex(characterParents);
    }

}

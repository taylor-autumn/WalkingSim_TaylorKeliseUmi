using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public enum gameState { normalMode, interactMode, menuMode}
public enum timeOfDay { beforeClass,inClass,lunch,gamesClub,evening,night }

public class gameManager : MonoBehaviour
{
    public gameState state;
    public timeOfDay gameLevel;
    public interactCircle currentInteract;
    public Animator theSun;
    GameObject characterParents;
    public int trackInteract = 0;
    movementScript moveRef;

    [Header("objects and text")]
    private GameObject michelle;
    public GameObject tvScreen;
    public GameObject modeBox;
    public GameObject nextLevelButton;
    public TMP_Text modeText;
    public TMP_Text trackingText;
    public TMP_Text maxInteractNumber;
    public GameObject pushButton;

    [Header("Transition stuff")]
    public GameObject blinkGO;
    public Animator blinkAnim;

    [Header("Menu Stuff")]
    public GameObject mainMenu;

    [Header("Audio")]
    public AudioSource dayMusic;
    public AudioSource noonMusic;
    public AudioSource lateMusic;
    public AudioSource currentMusic;
    public AudioSource vineBoom;
    public AudioSource elevatorMusic;
    public AudioSource smashMusic;
    public GameObject smashMusicArea;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state=gameState.normalMode;
        gameLevel=timeOfDay.beforeClass;
        michelle = GameObject.Find("Michelle");
        characterParents = GameObject.Find("characters");
        pushButton.SetActive(false);
        mainMenu.SetActive(false);
        changeCharPositions(characterParents);
        GameObject thePlayer = GameObject.Find("Player");
        moveRef = thePlayer.GetComponent<movementScript>();
        currentMusic = dayMusic;
        currentMusic.Play();
        smashMusicArea.SetActive(false);
        tvScreen.SetActive(false);
}

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.M) && !moveRef.inElev)
        {
            if (state == gameState.normalMode)
            {
                mainMenu.SetActive(true);
                state = gameState.menuMode;
            }
            else if (state == gameState.menuMode)
            {
                mainMenu.SetActive(false);
                state = gameState.normalMode;
            }
        }

        //cheat next level
        if (Input.GetKey(KeyCode.LeftShift) &&
            Input.GetKeyDown(KeyCode.P) &&
            state==gameState.normalMode && !moveRef.inElev)
        {
            hackNextLevel();
            print("hacking next level");
        }



        if (Input.GetKeyDown(KeyCode.P) && state == gameState.normalMode
            && !moveRef.inElev)
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

        if (gameLevel == timeOfDay.gamesClub || gameLevel == timeOfDay.evening || gameLevel == timeOfDay.night)
        {
            tvScreen.SetActive(true);
            smashMusicArea.SetActive(true);
        }
        else
        {
            tvScreen.SetActive(false);
            smashMusicArea.SetActive(false);
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

            //reset the character
            StartCoroutine(levelTransition());

            //animation reset elevator
            GameObject elevatorGO = GameObject.Find("elevatorGO");
            Animator elevAnim = elevatorGO.GetComponent<Animator>();
            elevAnim.Rebind();
            elevAnim.Update(0f);

            //music managing
            

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

        //reset the character
        StartCoroutine(levelTransition());

        //animation reset elevator
        GameObject elevatorGO = GameObject.Find("elevatorGO");
        Animator elevAnim = elevatorGO.GetComponent<Animator>();
        elevAnim.Rebind();
        elevAnim.Update(0f);
    }

    public IEnumerator levelTransition()
    {

        currentMusic.Pause();

        blinkGO.SetActive(true);
        blinkAnim.SetTrigger("blink");
        vineBoom.Play();

        yield return new WaitForSeconds(0.5f);
        GameObject thePlayer = GameObject.Find("Player");
        GameObject respawnPoint = GameObject.Find("respawn");

        if (thePlayer != null && respawnPoint != null)
        {

            CharacterController controller = thePlayer.GetComponent<CharacterController>();

            if (controller != null)
            {
                controller.enabled = false;
                thePlayer.transform.position = respawnPoint.transform.position;
                controller.enabled = true;
            }

            movementScript moveScript = thePlayer.GetComponent<movementScript>();
            if (moveScript != null)
            {
                moveScript.resetMoveDirection();
            }
        }

        yield return new WaitForSeconds(1.5f);
        blinkAnim.ResetTrigger("blink");
        blinkGO.SetActive(false);

        manageMusic();

    }

    public void manageMusic()
    {
        AudioSource newMusic = null;

        if (gameLevel == timeOfDay.beforeClass || gameLevel == timeOfDay.inClass)
            newMusic = dayMusic;
        else if (gameLevel == timeOfDay.lunch || gameLevel == timeOfDay.gamesClub)
            newMusic = noonMusic;
        else if (gameLevel == timeOfDay.evening || gameLevel == timeOfDay.night)
            newMusic = lateMusic;

        if (currentMusic == newMusic)
        {
            currentMusic.Play();
            return;
        }

        if (currentMusic != null)
            currentMusic.Stop();

        currentMusic = newMusic;

        if (currentMusic != null)
            currentMusic.Play();

    }

}

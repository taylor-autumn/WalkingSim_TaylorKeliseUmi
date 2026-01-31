using TMPro;
using UnityEngine;

public class interactCircle : MonoBehaviour
{
    [Header("UI GO's")]
    public GameObject interactButton;
    public GameObject dialogueUI;

    [Header("Other Shit")]
    public gameManager managerRef;
    private bool canPush = false;
    unitInfo charUnit;
    Animator diaAnimator;
    

    [Header("UI Texts")]
    public TMP_Text dialogueText;
    public TMP_Text nameText;
    public TMP_Text interactText;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        interactButton.SetActive(false);
        getInformation();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (canPush && Input.GetKeyDown(KeyCode.E))
        {
            managerRef.state = gameState.interactMode;
            print("In interacting");
            canPush = false;
            Animator interactAnimator = interactButton.GetComponentInChildren<Animator>();
            interactAnimator.SetTrigger("push");
            activateLines();
        }
        if (Input.GetKeyDown(KeyCode.Q) && managerRef.state == gameState.interactMode)
        {
            canPush = true;
            managerRef.state = gameState.normalMode;
            diaAnimator.SetTrigger("off");
            //make it so the interaction has to be finished to close it, make Q text visible at end and make it actually able to do it
        }
    }

    public void getInformation()
    {
        charUnit = gameObject.GetComponentInParent<unitInfo>();
        diaAnimator = dialogueUI.GetComponentInChildren<Animator>();
    }

    public void activateLines()
    {
        diaAnimator.SetTrigger("on");
        //make the box come in animation
        nameText.text = charUnit.charName;
        dialogueText.text = charUnit.charName + " is speaking...";

        if (managerRef.gameLevel == timeOfDay.beforeClass)
        {
            print(charUnit.charName + " before class lines go");
        }
        else if (managerRef.gameLevel == timeOfDay.inClass)
        {
            print(charUnit.charName + " class lines go");
        }
        else if (managerRef.gameLevel == timeOfDay.lunch)
        {
            print(charUnit.charName + " lunch lines go");
        }
        else if (managerRef.gameLevel == timeOfDay.gamesClub)
        {
            print(charUnit.charName + " games club lines go");
        }
        else if (managerRef.gameLevel == timeOfDay.evening)
        {
            print(charUnit.charName + " evening lines go");
        }
        else if (managerRef.gameLevel == timeOfDay.night)
        {
            print(charUnit.charName + " night lines go");
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactButton.SetActive(true);
            canPush = true;
            interactText.text = "Interact (E) with " + charUnit.charName;

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactButton.SetActive(false);
            canPush = false;
            print("Bye " + charUnit.name + "!");
        }
    }




}

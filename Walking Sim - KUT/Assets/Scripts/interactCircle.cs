using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class interactCircle : MonoBehaviour
{
    [Header("UI GO's")]
    public GameObject interactButton;
    public GameObject dialogueUI;
    public GameObject closeButton;

    [Header("Other Shit")]
    public gameManager managerRef;
    public bool canPush = false;
    unitInfo currentCharUnit;
    dialogueScript diaRef;
    Animator diaAnimator;
    public bool firstInteraction = true;

    [Header("UI Texts")]
    public TMP_Text nameText;
    public TMP_Text interactText;

    //event bool
    private bool dialogueFinished = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        interactButton.SetActive(false);
        getInformation();
    }
    void Start()
    {
        diaRef.enabled = false;
        closeButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (canPush && 
            Input.GetKeyDown(KeyCode.E) && 
            managerRef.currentInteract==this)
        {
            canPush = false;
            managerRef.state = gameState.interactMode;
            engageNPC();
        }
        if (Input.GetKeyDown(KeyCode.Q) && 
            managerRef.state == gameState.interactMode && 
            managerRef.currentInteract==this &&
            dialogueFinished)
        {
            canPush = true;
            exitDialogue();
            //make it so the interaction has to be finished to close it, make Q text visible at end and make it actually able to do it
        }

    }

    public void getInformation()
    {
        diaAnimator = dialogueUI.GetComponentInChildren<Animator>();
        GameObject gameManagerGO = GameObject.Find("gameManager");
        diaRef = gameManagerGO.GetComponent<dialogueScript>();

    }

    public void engageNPC()
    {
        dialogueFinished = false;
        diaRef.onDialogueFinished += handleDialogueFinished;

        Animator interactAnimator = interactButton.GetComponentInChildren<Animator>();
        interactAnimator.SetTrigger("push");
        diaRef.enabled = true;
        diaAnimator.SetTrigger("on");
        nameText.text = currentCharUnit.charName;
        diaRef.startTalking(currentCharUnit.idleImage, currentCharUnit.talkingImage);
        
        if (currentCharUnit.firstInteraction)
        {
            currentCharUnit.firstInteraction = false;
            managerRef.trackInteract += 1;
        }

        if (managerRef.gameLevel == timeOfDay.beforeClass)
        {
            print(currentCharUnit.charName + " before class lines go");
            diaRef.StartDialogue(currentCharUnit.firstLines);

        }
        else if (managerRef.gameLevel == timeOfDay.inClass)
        {
            print(currentCharUnit.charName + " class lines go");
            diaRef.StartDialogue(currentCharUnit.secondLines);

        }
        else if (managerRef.gameLevel == timeOfDay.lunch)
        {
            print(currentCharUnit.charName + " lunch lines go");
            diaRef.StartDialogue(currentCharUnit.thirdLines);
        }
        else if (managerRef.gameLevel == timeOfDay.gamesClub)
        {
            print(currentCharUnit.charName + " games club lines go");
            diaRef.StartDialogue(currentCharUnit.fourthLines);
        }
        else if (managerRef.gameLevel == timeOfDay.evening)
        {
            print(currentCharUnit.charName + " evening lines go");
            diaRef.StartDialogue(currentCharUnit.fifthLines);
        }
        else if (managerRef.gameLevel == timeOfDay.night)
        {
            print(currentCharUnit.charName + " night lines go");
            diaRef.StartDialogue(currentCharUnit.sixthLines);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            currentCharUnit = gameObject.GetComponentInParent<unitInfo>();
            managerRef.currentInteract=this;
            interactButton.SetActive(true);
            canPush = true;
            interactText.text = "Interact (E) with " + currentCharUnit.charName;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        managerRef.currentInteract = null;
        interactButton.SetActive(false);
        canPush = false;
    }

    void handleDialogueFinished()
    {
        dialogueFinished = true;
        closeButton.SetActive(true);
        //exit text font visible
    }

    public void exitDialogue()
    {
        if (managerRef.currentInteract != this) return;

        diaRef.onDialogueFinished -= handleDialogueFinished;

        managerRef.state = gameState.normalMode;
        diaAnimator.SetTrigger("off");
        diaRef.endDialogue();
        closeButton.SetActive(false);
    }
}

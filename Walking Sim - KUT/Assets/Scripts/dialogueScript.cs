using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

public class dialogueScript : MonoBehaviour
{
    public TMP_Text dialogueText;
    public float textSpeed;
    private int index;
    bool isTyping = false;
    public List<string> listOfChoice;
    gameManager managerRef;
    public event Action onDialogueFinished;

    [Header("Talking Image Thing")]
    public Image spritePlaceholder;
    public float talkingSpeed = 0.15f;
    Coroutine talkingRoutine;
    public Sprite idleSprite;
    public Sprite talkingSprite;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //listOfChoice

        dialogueText.text = string.Empty; //leah
        managerRef = gameObject.GetComponent<gameManager>();
        spritePlaceholder.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!enabled || listOfChoice==null) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (isTyping)
            {
                StopAllCoroutines();
                dialogueText.text = listOfChoice[index];
                isTyping = false;
                stopTalking();
            }
            else
            {
                NextLine();
            }
        }
    }

    public void StartDialogue(List<string> newLines)
    {
        StopAllCoroutines();

        listOfChoice = newLines;
        index = 0;

        dialogueText.text = string.Empty;

        if (listOfChoice == null || listOfChoice.Count == 0)
        {
            Debug.LogWarning("Dialogue started with empty list!");
            return;
        }

        StartCoroutine(TypeLine());
    }

    IEnumerator TypeLine()
    {
        isTyping = true;
        startTalking(idleSprite, talkingSprite);
        yield return null;

        foreach (char c in listOfChoice[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
        }
        isTyping = false;
        stopTalking();
        // kills itself if it's the only line in the list
        if (listOfChoice.Count == 1)
        {
            onDialogueFinished?.Invoke();
        }
    }
    void NextLine()
    {
        if (index < listOfChoice.Count-1)
        {
            index++;
            dialogueText.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        if (index == listOfChoice.Count - 1)
        {
            print("dialogue end");
            //event
            onDialogueFinished?.Invoke();
        }
    }

    public void endDialogue()
    {
        StopAllCoroutines();
        listOfChoice = null;
        index = 0;
        dialogueText.text = string.Empty;
        enabled = false;
        idleSprite = null;
        talkingSprite = null;
        stopTalking();
        spritePlaceholder.gameObject.SetActive(false);
    }

    public void startTalking(Sprite idle, Sprite talk)
    {
        spritePlaceholder.gameObject.SetActive(true);
        idleSprite = idle;
        talkingSprite = talk;
        // FORCE the first frame immediately
        spritePlaceholder.sprite = talkingSprite;
        talkingRoutine = StartCoroutine(talkingLoop());
    }
    public void stopTalking()
    {
        if (talkingRoutine != null)
        {
            StopCoroutine(talkingRoutine);
            talkingRoutine = null;
        }
        spritePlaceholder.sprite = idleSprite;
    }

    IEnumerator talkingLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(talkingSpeed);
            spritePlaceholder.sprite = idleSprite;
            yield return new WaitForSeconds(talkingSpeed);
            spritePlaceholder.sprite = talkingSprite;
        }
    }

}

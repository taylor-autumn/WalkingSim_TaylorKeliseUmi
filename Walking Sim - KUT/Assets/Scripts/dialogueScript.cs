using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;
using System;

public class dialogueScript : MonoBehaviour
{
    public TMP_Text dialogueText;
    public float textSpeed;
    private int index;
    public List<string> listOfChoice;
    gameManager managerRef;

    public event Action onDialogueFinished;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //listOfChoice

        dialogueText.text = string.Empty; //leah
        managerRef = gameObject.GetComponent<gameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!enabled || listOfChoice==null) return;

        if (Input.GetMouseButtonDown(0))
        {
            if (dialogueText.text == listOfChoice[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                dialogueText.text = listOfChoice[index];
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
        foreach (char c in listOfChoice[index].ToCharArray())
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(textSpeed);
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
    }


}

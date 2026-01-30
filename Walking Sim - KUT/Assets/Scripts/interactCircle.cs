using TMPro;
using UnityEngine;

public class interactCircle : MonoBehaviour
{
    

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
        }
        if (Input.GetKeyDown(KeyCode.Q) && managerRef.state == gameState.interactMode)
        {
            canPush = true;
            managerRef.state = gameState.normalMode;
        }
    }

    public void getInformation()
    {
        charUnit=gameObject.GetComponentInParent<unitInfo>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactButton.SetActive(true);
            canPush = true;
            interactText.text = "Interact (E) with " + charUnit.name;
            print(charUnit.firstLines[0]);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            interactButton.SetActive(false);
            canPush = false;
            print("Bye " +  charUnit.name + "!");
        }
    }




}

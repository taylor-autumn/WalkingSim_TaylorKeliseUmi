using JetBrains.Annotations;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class interactCircle : MonoBehaviour
{
    

public gameManager managerRef;
public GameObject interactButton;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   private void Awake()
    {
        interactButton.SetActive(false);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("in circle");
            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("left circle");
            interactButton.SetActive(false);
        }
    }




}

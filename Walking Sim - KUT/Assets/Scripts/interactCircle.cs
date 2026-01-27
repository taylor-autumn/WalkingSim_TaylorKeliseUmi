using UnityEngine;

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

    private void onTriggerEnter(Collider other)
    {
    if (other.CompareTag("Player"))
        {
            interactButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        interactButton.SetActive(false);
    }




}

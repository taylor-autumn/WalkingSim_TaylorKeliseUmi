using TMPro;
using UnityEngine;

public class elevator : MonoBehaviour
{
    private GameObject playerGO;
    private movementScript moveRef;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerGO = GameObject.Find("Player");
        moveRef = playerGO.GetComponent<movementScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerGO == null) print("the playerGO is null");
        if (moveRef == null) print("the move ref is null");
    }

    public void arriveAtFloor()
    {
        GameObject elevatorGO = GameObject.Find("elevatorGO");
        Animator elevAnim = elevatorGO.GetComponent<Animator>();
        
    }


}

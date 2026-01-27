using UnityEngine;

public enum gameState { normalMode, interactMode}
public class gameManager : MonoBehaviour
{
    public gameState state;
    //this script is where we interact and shit

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state=gameState.normalMode;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

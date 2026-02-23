using System.Collections.Generic;
using UnityEngine;

public class unitInfo : MonoBehaviour
{

    [Header("Name")]
    public string charName;

    [Header("Lines")]
    [TextArea(2,2)]
    public List<string> firstLines;
    [TextArea(2, 2)]
    public List<string> secondLines;
    [TextArea(2, 2)]
    public List<string> thirdLines;
    [TextArea(2, 2)]
    public List<string> fourthLines;
    [TextArea(2, 2)]
    public List<string> fifthLines;
    [TextArea(2, 2)]
    public List<string> sixthLines;

    [Header("Talking Images")]
    public Sprite idleImage;
    public Sprite talkingImage;

    [Header("Interaction")]
    public bool firstInteraction = true;

    [Header("Positions")]
    public List<GameObject> charPositions;
    public int charPositionIndex = 0;

    //public Queue<string> level1; tutorial stuff

}

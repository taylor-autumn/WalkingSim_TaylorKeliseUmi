using System.Collections.Generic;
using UnityEngine;

public class unitInfo : MonoBehaviour
{
    public string charName;
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

    //public Queue<string> level1; tutorial stuff

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //Allows us to see this script in the inspector.
public class Dialogue
{
    //public string name;

    [TextArea(3, 10)] //Min amount of lines to use in the inspector and max amount of lines to use.
    public string[] sentences;
}

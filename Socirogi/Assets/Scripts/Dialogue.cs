using UnityEngine;
using System.Collections; 
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class Dialogue
{
    public string name;
    public RawImage image;
    [TextArea(3, 10)]
    public string[] sentences;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
[Serializable]
public class DebugNode : Node
{
    public string GUID;
    public string DialogueText;
    public bool EntryPoint = false;
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEngine.UIElements;
[Serializable]
public class DialogueNode : Node
{
    public string GUID;
    public string DialogueText;
    public bool EntryPoint = false;
}

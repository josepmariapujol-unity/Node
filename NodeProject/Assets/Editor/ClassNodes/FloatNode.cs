using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEditor;
using System.Linq;
using UnityEngine.UIElements;
[Serializable]
public class FloatNode : Node
{
    public string GUID;
    public string FloatText;
    public bool EntryPoint = false;
}

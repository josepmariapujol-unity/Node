using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
[Serializable]
public class CurveNode : Node
{
    public string GUID;
    public bool EntryPoint = false;
}

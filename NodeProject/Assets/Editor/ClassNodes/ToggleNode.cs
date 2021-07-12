using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
[Serializable]
public class ToggleNode : Node
{
	public string GUID;
	public bool EntryPoint = false;
	public GameObject output;
	//public string name => "Color";
}
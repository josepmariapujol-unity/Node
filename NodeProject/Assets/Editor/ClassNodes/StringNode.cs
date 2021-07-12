using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

[Serializable]
public class StringNode : Node
{
	public string GUID;
	public string GameObjectText;
	public bool EntryPoint = false;
	public Edge edge;
	//public GameObject output;
	//public string name => "String";
}
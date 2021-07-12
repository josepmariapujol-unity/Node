using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;

[Serializable]
public class PrefabNode : Node
{
	public string GUID;
	public string GameObjectText;
	public bool EntryPoint = false;
	public GameObject output;
	//public string name => "Prefab";
}
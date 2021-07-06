using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CommentBlockData
{
    public List<string> ChildNodes = new List<string>();
    public Vector2 Position;
    public string Title= "Create Group";
}
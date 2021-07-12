using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

[Serializable]
public class StickyBlockData
{
    public string Title = "New Note";
    public StickyNoteFontSize FontSize = StickyNoteFontSize.Small;
    public string Contents = "Write something here";
}
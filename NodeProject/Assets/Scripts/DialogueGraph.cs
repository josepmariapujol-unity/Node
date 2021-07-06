using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEditor;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

public class DialogueGraph : EditorWindow
{
    private DialogueGraphView _graphView;

    [MenuItem("Assets/Create/Form/Blank Form Graph")]
    [MenuItem("Form Graph/New Form Graph")]

    public static void OpenDiagueGraphWindow()
    {
        Texture2D _idIcon =new Texture2D(16, 16);
        _idIcon.SetPixel(0, 0, Color.black);
        _idIcon.Apply();
        
        var window = GetWindow<DialogueGraph>();
        window.titleContent = new GUIContent("New Form Graph", _idIcon, "Create a Form");
    }

    private void OnEnable()
    {
        ConstructGraphView();
        GenerateToolbar();
        GenerateBlackBoard();
    }

    private void GenerateBlackBoard()
    {
        var blackboard = new Blackboard(_graphView);
        blackboard.title = "New Form Graph";
        blackboard.subTitle = "Form Graphs";
        blackboard.Add(new BlackboardSection{title = "Properties"});
        
        blackboard.addItemRequested = _blackboard => { _graphView.AddPropertyToBlackBoard(new ExposedProperty());};
        blackboard.Add(new BlackboardSection{title = "Keywords"});
        blackboard.editTextRequested = (blackboard1, element, newValue) =>
        {
            var oldPropertyName = ((BlackboardField) element).text;
            if (_graphView.ExposedProperties.Any(x => x.PropertyName == newValue))
            {
                EditorUtility.DisplayDialog("error", "rename again", "ok");
                return;
            }

            var propertyIndex = _graphView.ExposedProperties.FindIndex(x => x.PropertyName == oldPropertyName);
            _graphView.ExposedProperties[propertyIndex].PropertyName = newValue;
            ((BlackboardField) element).text = newValue;
        };
        
        blackboard.SetPosition(new Rect(new Vector2(20,40), new Vector2(200,300)));
        _graphView.Add(blackboard);
        _graphView.Blackboard = blackboard;
    }

    private void GenerateToolbar()
    {
        var toolbar = new Toolbar();

        var nodeSaveButton = new Button(() => { _graphView.SaveNode(); });
        nodeSaveButton.text = "Save Asset";
        
        var nodeSaveAsButton = new Button(() => { _graphView.SaveAsNode(); });
        nodeSaveAsButton.text = "Save As...";
        
        var nodeShowInProjectButton = new Button(() => { _graphView.ShowInProject(); });
        nodeShowInProjectButton.text = "Show In Project";
        
        var nodeBlackBoardButton = new Button(() => { _graphView.ShowBlackboard(); });
        nodeBlackBoardButton.text = "BlackBoard";
        
        var nodeCreateButton = new Button(() => { _graphView.CreateNodeDialogue("Dialogue from Toolbar", new Vector2(500,500)); });
        nodeCreateButton.text = "Create Dialogue";
        
        var nodeCreateButtonFloat = new Button(() => { _graphView.CreateNodeFloat("Float", new Vector2(500,500)); });
        nodeCreateButtonFloat.text = "Create Float";
        
        var nodeCreateButtonA = new Button(() => { _graphView.CreateNodeA("A", new Vector2(500,500)); });
        nodeCreateButtonA.text = "Create A";

        
        //toolbar.contentContainer.Add(nodeSaveButton);
        toolbar.Add(nodeSaveButton);
        toolbar.Add(nodeSaveAsButton);
        toolbar.Add(nodeShowInProjectButton);
        toolbar.Add(nodeBlackBoardButton);
        toolbar.Add(nodeCreateButton);
        toolbar.Add(nodeCreateButtonFloat);
        toolbar.Add(nodeCreateButtonA);

        rootVisualElement.Add(toolbar);
    }
    private void ConstructGraphView()
    {
        _graphView = new DialogueGraphView(this)
        {
            name = "Form Graph"
        };
        
        _graphView.StretchToParentSize();
        rootVisualElement.Add(_graphView);
    }

    private void OnDisable()
    {
        rootVisualElement.Remove(_graphView);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;

public class NodeSearchWindow : ScriptableObject, ISearchWindowProvider
{
    private DialogueGraphView _graphView;
    private EditorWindow _window;
    public Texture2D[] _indentationIcon = new Texture2D[2];
    
    public void Init(EditorWindow window, DialogueGraphView graphView)
    {
        _graphView = graphView;
        _window = window;

        for (int i = 0; i < _indentationIcon.Length; i++)
        {
            _indentationIcon[i] = new Texture2D(1, 1);
            _indentationIcon[i].SetPixel(0,0,new Color(255-(i*200),0+(i*200),0+(i*200),1f));

            _indentationIcon[i].Apply();
        }
        /*
        _indentationIcon[0] = new Texture2D(1, 1);
        _indentationIcon[0].SetPixel(0,0,new Color(255,0,0,1f));
        _indentationIcon[0].Apply();
        */
    }
    
    public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
    {
        var tree = new List<SearchTreeEntry>
        {
            new SearchTreeGroupEntry(new GUIContent("Create Elements"), 0),
            
            new SearchTreeGroupEntry(new GUIContent("Node", _indentationIcon[0]), 1),
            new SearchTreeEntry(new GUIContent("Create ANODE", _indentationIcon[0]))
            {
                userData = new ANode(), level = 2,
            },
            new SearchTreeEntry(new GUIContent("Create Gradient", _indentationIcon[0]))
            {
                userData = new GradientNode(), level = 2,
            },
            new SearchTreeEntry(new GUIContent("Create Bool", _indentationIcon[0]))
            {
                userData = new BoolNode(), level = 2,
            },
            new SearchTreeEntry(new GUIContent("Create Color", _indentationIcon[0]))
            {
                userData = new ColorNode(), level = 2,
            },
            new SearchTreeEntry(new GUIContent("Create Curve", _indentationIcon[1]))
            {
                userData = new CurveNode(), level = 2,
            },
            new SearchTreeEntry(new GUIContent("Create Dialogue", _indentationIcon[0]))
            {
                userData = new DialogueNode(), level = 2,
            },
            new SearchTreeEntry(new GUIContent("Create Float", _indentationIcon[0]))
            {
                userData = new FloatNode(), level = 2,
            },
            new SearchTreeEntry(new GUIContent("Create Prefab", _indentationIcon[0]))
            {
                userData = new PrefabNode(), level = 2,
            },
            new SearchTreeEntry(new GUIContent("Create Text", _indentationIcon[0]))
            {
                userData = new StringNode(), level = 2,
            },
            new SearchTreeEntry(new GUIContent("Create Vector", _indentationIcon[0]))
            {
                userData = new VectorNode(), level = 2,
            },
            
            new SearchTreeGroupEntry(new GUIContent("Group",_indentationIcon[0]), 1),
            new SearchTreeEntry(new GUIContent("Create Group",_indentationIcon[0]))
            {
                userData = new Group(), level = 2,
            },
            
            new SearchTreeGroupEntry(new GUIContent("Sticky Note",_indentationIcon[0]), 1),
            new SearchTreeEntry(new GUIContent("Create Sticky Note", _indentationIcon[0]))
            {
                userData = new StickyNote(), level = 2,
            },
            
            new SearchTreeGroupEntry(new GUIContent("Stack Node",_indentationIcon[0]), 1),
            new SearchTreeEntry(new GUIContent("Create Stack Node", _indentationIcon[0]))
            {
                userData = new StackNode(), level = 2,
            },
        };
        return tree;
    }

    public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
    {
        var worldMousePosition = _window.rootVisualElement.ChangeCoordinatesTo(_window.rootVisualElement.parent,
            context.screenMousePosition - _window.position.position);

        var localMousePosition = _graphView.contentContainer.WorldToLocal(worldMousePosition);
        
        switch (SearchTreeEntry.userData)
        {
            case ANode aNode:
                _graphView.CreateNodeA("node", localMousePosition);
                return true;
            case GradientNode gradientNode:
                _graphView.CreateNodeGradient("Gradient", localMousePosition);
                return true;
            case BoolNode boolNode:
                _graphView.CreateNodeBool("Bool", localMousePosition);
                return true;
            case ColorNode colorNode:
                _graphView.CreateNodeColor("Color", localMousePosition);
                return true;            
            case CurveNode curveNode:
                _graphView.CreateNodeCurve("Curve", localMousePosition);
                Debug.Log("curve from Search window");
                return true;
            case DialogueNode dialogueNode:
                _graphView.CreateNodeDialogue("Dialogue from SearchBar", localMousePosition);
                return true;
            case FloatNode floatNode:
                _graphView.CreateNodeFloat("Float", localMousePosition);
                return true;
            case PrefabNode prefabNode:
                _graphView.CreateNodeGameObject("GameObject", localMousePosition);
                return true;
            case StringNode stringNode:
                _graphView.CreateNodeString("String", localMousePosition);
                return true;
            case VectorNode vectorNode:
                _graphView.CreateNodeVector("Vector", localMousePosition);
                return true;
            case Group group:
                var rect = new Rect(localMousePosition, _graphView.DefaultCommentBlockSize);
                _graphView.CreateCommentBlock(rect);
                return true;
            case StickyNote note:
                var rectSticky = new Rect(localMousePosition, _graphView.DefaultCommentBlockSize);
                _graphView.CreateStickyNote(rectSticky);
                return true;
            case StackNode stack:
                var rectStack = new Rect(localMousePosition, _graphView.DefaultCommentBlockSize);
                _graphView.CreateStackNode(rectStack);
                return true;
            default:
                return false;
        }
    }
}
 
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEditor;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEditor.UIElements;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;
using Button = UnityEngine.UIElements.Button;
using Toggle = UnityEngine.UIElements.Toggle;
using Slider = UnityEngine.UIElements.Slider;
using RawImage = UnityEngine.UIElements.Image;
using Image = UnityEngine.UI.Image;

public class DialogueGraphView : GraphView
{
    private readonly Vector2 defaultNodeSize = new Vector2(100, 200);
    public readonly Vector2 DefaultCommentBlockSize = new Vector2(300, 200);
    
    public readonly List<ExposedProperty> ExposedProperties = new List<ExposedProperty>();
    
    public Blackboard Blackboard;
    
    private NodeSearchWindow _searchWindow;
    
    public DialogueGraphView(EditorWindow editorWindow)
    {
        SetupZoom(ContentZoomer.DefaultMinScale, ContentZoomer.DefaultMaxScale);
        this.AddManipulator(new ContentDragger());
        this.AddManipulator(new SelectionDragger());
        this.AddManipulator(new RectangleSelector());
        this.AddManipulator(new FreehandSelector());
        this.AddManipulator(new ContentZoomer());
        
        var miniMap = new MiniMap();
        miniMap.SetPosition(new Rect(20, 400, 200, 200));
        this.Add(miniMap);
  
        var grid = new GridBackground();
        Insert(0, grid);
        
        AddElement(GenerateString());
        AddElement(GenerateDebug());

        AddSearchWindow(editorWindow);
    }
    private void AddSearchWindow(EditorWindow editorWindow)
    {
        _searchWindow = ScriptableObject.CreateInstance<NodeSearchWindow>();
        _searchWindow.Init(editorWindow, this);
        nodeCreationRequest = context =>
            SearchWindow.Open(new SearchWindowContext(context.screenMousePosition), _searchWindow);  
    }
    
    public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
    {
        var compatiblePorts = new List<Port>();
        ports.ForEach((port) =>
        {
            if (startPort != port && startPort.node != port.node)
                compatiblePorts.Add(port);
        });
        return compatiblePorts;
    }
    
    private Port GeneratePort(DebugNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(GameObject));
    }
    private Port GeneratePort(StringNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Multi)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(GameObject));
    }
    private Port GeneratePort(DialogueNode node, Direction portDirection, Port.Capacity capacity = Port.Capacity.Single)
    {
        return node.InstantiatePort(Orientation.Horizontal, portDirection, capacity, typeof(GameObject));
    }
    public void CreateNodeA(string nodeName, Vector2 position)
    {
        AddElement(GenerateA());
    }
    public void CreateNodeGradient(string nodeName, Vector2 position)
    {
        AddElement(GenerateGradient());
    }
    public void CreateNodeBool(string nodeName, Vector2 position)
    {
        AddElement(GenerateBool());
    }
    public void CreateNodeColor(string nodeName, Vector2 position)
    {
        AddElement(GenerateColor());
    }
    public void CreateNodeCurve(string nodeName, Vector2 position)
    {
        AddElement(GenerateCurve());
    }
    public void CreateNodeDialogue(string nodeName, Vector2 position)
    {
        AddElement(CreateDialogueNode("Dialogue", position));
    }
    public void CreateNodeFloat(string nodeName, Vector2 position)
    {
        AddElement(GenerateFloat());
    }
    public void CreateNodeGameObject(string nodeName, Vector2 position)
    {
        AddElement(GenerateGameObject());
    }    
    public void CreateNodeDebug(string nodeName, Vector2 position)
    {
        AddElement(GenerateDebug());
    }  
    public void CreateNodeString(string nodeName, Vector2 position)
    {
        AddElement(GenerateString());
    }  
    public void CreateNodeVector(string nodeName, Vector2 position)
    {
        AddElement(GenerateVector());
    }
    private DebugNode GenerateTogle()
    {
        var node = new DebugNode()
        {
            title = "Debug"
        };

        var text = new Label("null");
        
        //Insert Input Port
        var inputPort = GeneratePort(node, Direction.Input, Port.Capacity.Single);
        inputPort.portName = "obj";
        inputPort.portColor = Color.red;
        
        node.mainContainer.Add(text);
        node.inputContainer.Add(inputPort);
        
        node.RefreshExpandedState();
        node.RefreshPorts();
        
        node.SetPosition(new Rect(850,100,600,300));

        return node;
    }
    private ANode GenerateA()
    {
        var node = new ANode()
        {
            title = "Testing Features                                                  _",
        };
        var objectField = new PropertyField();
        List<string> list = new List<string>(new string[3]);
        list.Add("1");
        list.Add("2");
        //objectField.contentContainer.Add(new Resizer(new Vector2(10,10)));
        objectField.contentContainer.Add(new Foldout());
        objectField.contentContainer.Add(new Label("hey"));
        objectField.contentContainer.Add(new Pill());
        objectField.contentContainer.Add(new Scroller());
        objectField.contentContainer.Add(new BoundsField("go"));
        objectField.contentContainer.Add(new DoubleField(13));
        objectField.contentContainer.Add(new DropdownField("Dropdown",list,1));
        objectField.contentContainer.Add(new EnumField());
        objectField.contentContainer.Add(new LayerField("LayerField",3));
        objectField.contentContainer.Add(new LongField("Long Field",10));
        objectField.contentContainer.Add(new TagField("TagField","1"));
        objectField.contentContainer.Add(new ToolbarPopupSearchField());
        objectField.contentContainer.Add(new RadioButton() );
        objectField.contentContainer.Add(new RepeatButton());
        objectField.contentContainer.Add(new TabButton());
        objectField.contentContainer.Add(new RectField("RectField"));
        objectField.contentContainer.Add(new EnumFlagsField("EnumFlagsField"));
        objectField.contentContainer.Add(new Button(){text = "Static/Non-Static"});
        objectField.contentContainer.Add(new Toggle(){text = "Static/Non-Static"});
        objectField.contentContainer.Add(new Slider());
        
        node.contentContainer.Add(objectField);
        
        node.RefreshExpandedState();
        node.RefreshPorts();

        node.SetPosition(new Rect(400, 300, 300, 300));
        return node;  
    }
    
    private GradientNode GenerateGradient()
    {
        var node = new GradientNode()
        {
            title = "Gradient",
        };
        var objectField = new PropertyField();
        
        objectField.contentContainer.Add(new GradientField("Gradient"));
        node.contentContainer.Add(objectField);
        
        node.RefreshExpandedState();
        node.RefreshPorts();
        
        node.SetPosition(new Rect(400,300,300,300));
        return node;  
    }
    private PrefabNode GenerateGameObject()
    {
        var node = new PrefabNode()
        {
            title = "Game Object",
        };
        var objectField = new ObjectField
        {
            label = "Output",
            objectType = typeof(GameObject)
        };
        node.outputContainer.Add(objectField);
        
        node.RefreshExpandedState();
        node.RefreshPorts();
        
        node.SetPosition(new Rect(400,300,100,150));
        return node;
    }
    
    private FloatNode GenerateEntryPointFloatNode()
    {
        var node = new FloatNode()
        {
            title = "Float",
            GUID = Guid.NewGuid().ToString(),
            FloatText = "Entry",
            EntryPoint = true
        };
        
        node.RefreshExpandedState();
        node.RefreshPorts();
        
        node.SetPosition(new Rect(400,200,150,300));
        return node;
    }
    private CurveNode GenerateCurve()
    {
        var node = new CurveNode()
        {
            title = "Curve                                                  _",
        };
        var objectField = new PropertyField();
        
        objectField.contentContainer.Add(new CurveField("curve"));
        node.contentContainer.Add(objectField);
        
        node.RefreshExpandedState();
        node.RefreshPorts();
        
        node.SetPosition(new Rect(400,300,300,300));
        return node;    
    }
    private BoolNode GenerateBool()
    {
        Debug.Log("bool");
        var node = new BoolNode()
        {
            title = "Bool",
        };

        var objectField = new PropertyField();
        
        //objectField.contentContainer.Add(new CurveField("curve"));
        node.outputContainer.Add(objectField);
        
        node.RefreshExpandedState();
        node.RefreshPorts();
        
        node.SetPosition(new Rect(400,300,300,150));
        
        return null;    
    }

    private DebugNode GenerateDebug()
    {
        var node = new DebugNode()
        {
            title = "Debug"
        };

        var text = new Label("null");
        
        //Insert Input Port
        var inputPort = GeneratePort(node, Direction.Input, Port.Capacity.Single);
        inputPort.portName = "obj";
        inputPort.portColor = Color.red;
        
        node.mainContainer.Add(text);
        node.inputContainer.Add(inputPort);
        
        node.RefreshExpandedState();
        node.RefreshPorts();
        
        node.SetPosition(new Rect(850,100,600,300));

        return node;
    }
    
    private StringNode GenerateString()
    {
        var node = new StringNode() {title = "Text"};
        //Insert Output Port
        var outputPort = GeneratePort(node, Direction.Output, Port.Capacity.Multi);
        outputPort.portColor = Color.red;
        
        //Remove outputName
        var oldLabel = outputPort.contentContainer.Q<Label>("type");
        outputPort.contentContainer.Remove(oldLabel);

        //Add Text Field
        var textField = new TextField()
        {
            label = "Output",
            name = string.Empty,
            value = " Insert Text "
        };

        textField.RegisterValueChangedCallback(evt =>
        {
            //node.title = evt.newValue;
            outputPort.portName = evt.newValue;
        });
        
        outputPort.contentContainer.Add(new Label("   "));
        outputPort.contentContainer.Add(textField);
        
        //textField.SetValueWithoutNotify("Text Example Box");
        node.contentContainer.Add(outputPort);
        
        //Style Node Color
        node.styleSheets.Add(Resources.Load<StyleSheet>("StringNode"));

        var buttonInput = new Button(() => { StaticButton(node); }) {text = "Static/Non-Static"};
        var button2 = new Button(() => { DeleteButton(node); }) {text = "Remove"};
        node.titleButtonContainer.Add(buttonInput);
        node.titleButtonContainer.Add(button2);
        
        node.RefreshExpandedState();
        node.RefreshPorts();
        
        node.SetPosition(new Rect(400,100,600,300));
        return node;
    }

    private void DeleteButton(StringNode stringNode)
    {
        Debug.Log(stringNode.edge.output.capacity + " stringNode.edge.output.capacity");
        //Remove(stringNode);
    }
    

    bool _staticBool = true;
    private void StaticButton(StringNode stringNode)
    {
        if (_staticBool)
        {
            stringNode.capabilities &= Capabilities.Movable;
            stringNode.capabilities &= Capabilities.Deletable;
            _staticBool = false;
        }
        else
        {
            stringNode.capabilities = ~Capabilities.Movable;
            stringNode.capabilities = ~Capabilities.Deletable;
            _staticBool = true;
        }
    }
    private DialogueNode CreateDialogueNode(string nodeName, Vector2 position)
    {
        var dialogueNode = new DialogueNode()
        {
            title = nodeName,
            DialogueText = nodeName,
            GUID = Guid.NewGuid().ToString(),
            EntryPoint = true
        };

        var inputPort = GeneratePort(dialogueNode, Direction.Input, Port.Capacity.Multi);
        inputPort.portName = "Input";
        inputPort.portColor = Color.magenta;
        dialogueNode.inputContainer.Add(inputPort);

        var outputPort = GeneratePort(dialogueNode, Direction.Output, Port.Capacity.Multi);
        outputPort.portName = "Output";
        outputPort.portColor = Color.magenta;
        dialogueNode.outputContainer.Add(outputPort);
        
        dialogueNode.styleSheets.Add(Resources.Load<StyleSheet>("DialogueNode"));

        var buttonInput = new Button(() => { AddChoicePort(dialogueNode, Direction.Input); }) {text = "Add Input"};

        var buttonOutput = new Button(() => { AddChoicePort(dialogueNode, Direction.Output); }) {text = "Add Output"};

        var nodeCreateButton =
            new Button(() => { CreateNodeDialogue("Dialogue from Node", position); }) {text = "Create Dialogue"};

        var textField = new TextField("Text");
        
        textField.RegisterValueChangedCallback(evt =>
        {
            //dialogueNode.DialogueText = evt.newValue;
            dialogueNode.title = evt.newValue;
        });
        textField.SetValueWithoutNotify(dialogueNode.title);
        
        dialogueNode.mainContainer.Add(textField);

        dialogueNode.titleContainer.Add(buttonInput);
        dialogueNode.titleContainer.Add(buttonOutput);

        dialogueNode.Add(nodeCreateButton);
        
        dialogueNode.RefreshExpandedState();
        dialogueNode.RefreshPorts();
        
        dialogueNode.SetPosition(new Rect(position, defaultNodeSize));
        return dialogueNode;
    }
    private FloatNode GenerateFloat()
    {
        var node = new FloatNode()
        {
            title = "Float",
        };
        
        var objectField = new FloatField()
        {
            label = "Output",
        };
        node.outputContainer.Add(objectField);
        
        node.RefreshExpandedState();
        node.RefreshPorts();
        
        node.SetPosition(new Rect(400,300,100,150));
        return node;
    }

    private ColorNode GenerateColor()
    {
        var node = new ColorNode()
        {
            title = "Color",
        };
        
        var objectField = new ColorField()
        {
            label = "Output",
        };
        node.outputContainer.Add(objectField);
        
        node.RefreshExpandedState();
        node.RefreshPorts();
        
        node.SetPosition(new Rect(400,300,100,150));
        return node;
    }
    
    private VectorNode GenerateVector()
    {
        var node = new VectorNode()
        {
            title = "Vector3",
        };
        var objectField = new Vector3Field()
        {
            label = "Output",
        };
        node.outputContainer.Add(objectField);

        node.RefreshExpandedState();
        node.RefreshPorts();
        
        node.SetPosition(new Rect(400,300,100,150));
        return node;    
    }
    //__________________________________________________________________________________________________________________________________________________________________________________________________________________________________
    public Group CreateCommentBlock(Rect rect, CommentBlockData commentBlockData = null)
    {
        if (commentBlockData == null)
            commentBlockData = new CommentBlockData();
        
        var group = new Group
        {
            autoUpdateGeometry = true,
            title = commentBlockData.Title,
        };

        var objectField = new ColorField();
        group.headerContainer.Add(objectField);

        group.style.color = new StyleColor(Color.green);


        AddElement(group);

        group.SetPosition(rect);
        return group;
    }
    
    public StickyNote CreateStickyNote(Rect rect, StickyBlockData stickyNoteData = null)
    {
        if (stickyNoteData == null)
           stickyNoteData = new StickyBlockData();
        var stickyNode = new StickyNote
        {
            title = stickyNoteData.Title,
            contents = stickyNoteData.Contents,
            fontSize = stickyNoteData.FontSize
        };
        AddElement(stickyNode);
        stickyNode.SetPosition(rect);
        return stickyNode;
    }
    public StackNode CreateStackNode(Rect rect, StackBlockData stackNoteData = null)
    {
        if (stackNoteData == null)
            stackNoteData = new StackBlockData();
        var stackNode = new StackNode()
        {
            title = stackNoteData.Title,
        };

        stackNode.headerContainer.Add(new Label("Stack"));
        AddElement(stackNode);
        stackNode.SetPosition(rect);
        return stackNode;
    }
//__________________________________________________________________________________________________________________________________________________________________________________________________________________________________
    private void AddChoicePort(DialogueNode dialogueNode, Direction direction)
    {
        var generatedPort = GeneratePort(dialogueNode, direction, Port.Capacity.Multi);

        if (direction == Direction.Input)
        {
            var inputPortCount = dialogueNode.inputContainer.Query("connector").ToList().Count;
            generatedPort.portName = $"Input {inputPortCount}";
            dialogueNode.inputContainer.Add(generatedPort);
        }
        else
        {
            var outPortCount = dialogueNode.outputContainer.Query("connector").ToList().Count;
            generatedPort.portName = $"Output {outPortCount}";
            dialogueNode.outputContainer.Add(generatedPort);
        }
        
        dialogueNode.RefreshExpandedState();
        dialogueNode.RefreshPorts();
    }

    public void AddPropertyToBlackBoard(ExposedProperty exposedProperty)
    {
        var localPropertyName = exposedProperty.PropertyName;
        var localPropertyNameBase = exposedProperty.PropertyName;
        var localPropertyValue = exposedProperty.PropertyValue;
        var i = 1;
        while (ExposedProperties.Any(x=>x.PropertyName == localPropertyName))
        {
            localPropertyName = $"{localPropertyNameBase} " + $"({i})";
            i += 1;
        }
        
        var property = new ExposedProperty();
        property.PropertyName = localPropertyName;
        property.PropertyValue = localPropertyValue;
        ExposedProperties.Add(property);

        var container = new VisualElement();
        var blackboardField = new BlackboardField {text = property.PropertyName, typeText = "string"};
        container.Add(blackboardField);

        var propertyValueTextField = new TextField("Value")
        {
            value = localPropertyValue
        };
        propertyValueTextField.RegisterValueChangedCallback(evt =>
        {
            var changingPropertyIndex = ExposedProperties.FindIndex(x => x.PropertyName == property.PropertyName);
            ExposedProperties[changingPropertyIndex].PropertyValue = evt.newValue;
        });
        
        var blackBoardValueRow = new BlackboardRow(blackboardField, propertyValueTextField);
        container.Add(blackBoardValueRow);

        Blackboard.Add(container);
    }
//__________________________________________________________________________________________________________________________________________________________________________________________________________________________________
    public void ShowInProject()
    {
        Debug.Log("Show in project");
    }
    public void ShowBlackboard()
    {
        Debug.Log("BlackBoard");
    }
    public void SaveNode()
    {
        Debug.Log("Save Asset");
    }
    public void SaveAsNode()
    {
        Debug.Log("Save As...");
    }
}

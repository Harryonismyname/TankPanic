using UnityEditor;
using UnityEngine;
using System.IO;

public class EventChannelGeneratorWindow : EditorWindow
{
    private string typeName = "MyCustomType";
    private string outputDirectory = "Assets/Scripts/EventChannels";

    [MenuItem("Tools/Event Channels/Generate EventChannel Wrapper")]
    public static void ShowWindow()
    {
        GetWindow<EventChannelGeneratorWindow>("EventChannel Generator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Create Concrete EventChannel<T> Wrapper", EditorStyles.boldLabel);

        typeName = EditorGUILayout.TextField("Type Name", typeName);
        outputDirectory = EditorGUILayout.TextField("Output Directory", outputDirectory);

        GUILayout.Space(10);

        if (GUILayout.Button("Generate"))
        {
            GenerateEventChannel(typeName, outputDirectory);
        }
    }

    private void GenerateEventChannel(string type, string folder)
    {
        if (string.IsNullOrWhiteSpace(type))
        {
            Debug.LogWarning("Type name cannot be empty.");
            return;
        }

        string className = $"{type}EventChannel";
        string safeFolder = folder.Replace("Assets", "").Trim('/');
        string fullPath = Path.Combine("Assets", safeFolder);

        if (!Directory.Exists(fullPath))
            Directory.CreateDirectory(fullPath);

        string filePath = Path.Combine(fullPath, $"{className}.cs");

        string content = $@"using UnityEngine;

[CreateAssetMenu(menuName = ""ScriptableObjects/Events/{className}"")]
public class {className} : EventChannel<{type}> {{ }}
";

        File.WriteAllText(filePath, content);
        AssetDatabase.Refresh();

        Debug.Log($"✅ Created EventChannel wrapper: {className} at {filePath}");
        EditorUtility.DisplayDialog("Success", $"Generated {className} in {fullPath}", "OK");
    }
}

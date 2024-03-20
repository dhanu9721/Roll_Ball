using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class ScriptableObjectToJsonConverter : MonoBehaviour
{
    public ScriptableObject myScriptableObject; // Assign your ScriptableObject in the Inspector

    private void Start()
    {
        ConvertToJson();
    }
    public void ConvertToJson()
    {
        string json = JsonUtility.ToJson(myScriptableObject);
        string folderPath = Application.dataPath + "/Scripts/Levels"; // Path to the folder
        string fileName = "Level_1.json"; // File name

        // Combine folder path and file name
        string filePath = Path.Combine(folderPath, fileName);

        // Ensure the directory exists
        Directory.CreateDirectory(folderPath);

        // Write the JSON string to a file
        File.WriteAllText(filePath, json);

        Debug.Log("ScriptableObject converted to JSON and saved to: " + filePath);
    }
}

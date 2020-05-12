using System;
using System.IO;
using ProgressGrid;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(NoteProgressGridData))]
public class NoteProgressGridEditor : Editor {
    public override void OnInspectorGUI() {
        DrawDefaultInspector();
        if (GUILayout.Button("To json")) {
            string json = JsonUtility.ToJson(target);
            File.WriteAllText($"{Application.dataPath}/SerializedData/note_grid_data.json", json);
        }

        if (GUILayout.Button("From json")) {
            string json;
            try {
                json = File.ReadAllText($"{Application.dataPath}/SerializedData/note_grid_data.json");
            } catch (Exception e) {
                Debug.Log("No data to deserialize");
                return;
            }
            JsonUtility.FromJsonOverwrite(json, target);
        }
    }
}
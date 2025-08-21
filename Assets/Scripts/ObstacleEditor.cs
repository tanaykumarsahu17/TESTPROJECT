using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Custom editor window for visually editing obstacle grids stored in ObstacleData
// Opens via the Unity menu under Tools > Obstacle Editor
public class ObstacleEditorWindow : EditorWindow
{
    private ObstacleData data; //obstacledata asset being edited

    [MenuItem("Tools/Obstacle Editor")] //Adds a menu option to open the Obstacle editor window
    public static void OpenWindow()
    {
        GetWindow<ObstacleEditorWindow>("Obstacle Editor");
    }

    private void OnGUI() //unity callback for drawing and handling the editor UI
    {
        data =(ObstacleData)EditorGUILayout.ObjectField("Obstacle Data", data, typeof(ObstacleData), false); //field to assign/load the ObstacleData asset

        if (data == null) return; //exit if no data assigned

        for (int z = 0; z<data.gSize; z++) //draw grid UI row by row
        {
            EditorGUILayout.BeginHorizontal();
            for (int x = 0; x < data.gSize; x++)
            {
                bool value =data.GetObstacle(x, z); //current cell state
                bool newVal =GUILayout.Toggle(value, "", GUILayout.Width(20)); //draw a toggle button(checkbox) for this cell
                if (newVal != value) //if value changed record for undo and update asset
                {
                    Undo.RecordObject(data, "Toggle Obstacle");
                    data.SetObstacle(x, z, newVal);
                    EditorUtility.SetDirty(data); //for unity to save the changes
                }
            }
            EditorGUILayout.EndHorizontal();
        }
    }
}



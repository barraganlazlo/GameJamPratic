using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Castle))]
public class CastleEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Castle castle = (Castle)target;
        if (GUILayout.Button("Create Sides"))
        {
            castle.CreateSides();
        }
    }
}

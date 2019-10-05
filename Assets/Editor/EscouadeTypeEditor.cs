using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EscouadeType))]
public class EscouadeTypeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        EscouadeType escoudeType = (EscouadeType)target;
        int[,] units = escoudeType.units;
        GUILayout.Label("units :");
        for (int i = 0; i < units.GetLength(0); i++)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < units.GetLength(1); j++)
            {
                units[i, j]=EditorGUILayout.IntField(units[i,j], GUILayout.Width(30));
            }
            GUILayout.EndHorizontal();
        }
        EditorUtility.SetDirty(escoudeType);
    }
    void DrawUnitType()
    {
    }
}

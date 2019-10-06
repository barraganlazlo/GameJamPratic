using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EscouadeType))]
public class EscouadeTypeEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EscouadeType escoudeType = (EscouadeType)target;
        int[] units = escoudeType.units;
        SerializedProperty serializedUnits = serializedObject.FindProperty("units");
        SerializedProperty serializedHeight = serializedObject.FindProperty("height");
        SerializedProperty serializedWidth = serializedObject.FindProperty("width");
        SerializedProperty serializedWaveBegin = serializedObject.FindProperty("waveBegin");
        SerializedProperty serializedWaveEnd = serializedObject.FindProperty("waveEnd");
        SerializedProperty serializedTauxApparition = serializedObject.FindProperty("tauxApparition");

        GUILayout.BeginHorizontal();
        GUILayout.Label("Taux Apparition :", GUILayout.Width(80));
        serializedTauxApparition.intValue = EditorGUILayout.IntField(serializedTauxApparition.intValue, GUILayout.Width(60));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("WaveBegin :", GUILayout.Width(70));
        serializedWaveBegin.intValue = EditorGUILayout.IntField(serializedWaveBegin.intValue, GUILayout.Width(60));
        GUILayout.Label("WaveEnd :", GUILayout.Width(70));
        serializedWaveEnd.intValue = EditorGUILayout.IntField(serializedWaveEnd.intValue, GUILayout.Width(60));
        GUILayout.EndHorizontal();

        GUILayout.BeginHorizontal();
        GUILayout.Label("Height :", GUILayout.Width(60));
        int height = EditorGUILayout.IntField(serializedHeight.intValue, GUILayout.Width(60));
        GUILayout.Label("Width :", GUILayout.Width(60));
        int width= EditorGUILayout.IntField(serializedWidth.intValue, GUILayout.Width(60));
        GUILayout.EndHorizontal();

        serializedHeight.intValue = height;
        serializedWidth.intValue = width;
        serializedUnits.arraySize =height * width ;

        GUILayout.Label("units :");
        for (int i = 0; i < height; i++)
        {
            GUILayout.BeginHorizontal();
            for (int j = 0; j < width; j++)
            {
                serializedUnits.GetArrayElementAtIndex(i * width + j).intValue=EditorGUILayout.IntField(serializedUnits.GetArrayElementAtIndex(i * width + j).intValue, GUILayout.Width(30));
            }
            GUILayout.EndHorizontal();
        }
        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(escoudeType);
    }
}

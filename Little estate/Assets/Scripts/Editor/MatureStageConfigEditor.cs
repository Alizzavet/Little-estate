using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MatureStageConfig))]
public class MatureStageConfigEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        MatureStageConfig config = (MatureStageConfig)target;

        EditorGUILayout.PropertyField(serializedObject.FindProperty("_stageName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_sprite"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_priceCost"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_timeToNextStage"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_lootName"));
        
        config.MinLootCount = EditorGUILayout.IntSlider("Min Loot Count", config.MinLootCount, 
            1, Mathf.Max(config.MaxLootCount, 1));
        config.MaxLootCount = EditorGUILayout.IntSlider("Max Loot Count", config.MaxLootCount, 
            config.MinLootCount, int.MaxValue);

        serializedObject.ApplyModifiedProperties();
    }
}
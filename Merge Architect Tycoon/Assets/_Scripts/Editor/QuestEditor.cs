using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Quest))]
public class QuestEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        Quest quest = (Quest)target;
        EditorGUILayout.PropertyField(serializedObject.FindProperty("questType"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("questName"));
        EditorGUILayout.PropertyField(serializedObject.FindProperty("rewards"));

        switch (quest.questType)
        {
            case QuestType.BuildingQuest:
                EditorGUILayout.PropertyField(serializedObject.FindProperty("buildingName"));
                EditorGUILayout.PropertyField(serializedObject.FindProperty("buildingImage"));

                break;
            case QuestType.CreateItemQuest:
                DrawCreateItemQuestFields(serializedObject.FindProperty("itemsToMerge"));
                DrawCreateItemQuestFields(serializedObject.FindProperty("itemsCount"));
                break;
        }

        serializedObject.ApplyModifiedProperties();
    }


    void DrawCreateItemQuestFields(SerializedProperty questNameProp)
    {
        EditorGUILayout.PropertyField(questNameProp);
    }
}
using _Scripts.Logic.Merge.Items;
using _Scripts.Logic.Merge.MergePlane;
using UnityEditor;
using UnityEngine;

namespace _Scripts.Editor
{
    [CustomEditor(typeof(MergeLevel))]
    public class InitialItemDropEditor : UnityEditor.Editor
    {

        public bool showLevels = true;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            MergeLevel InitialItemDrop = (MergeLevel)target;
            EditorGUILayout.Space();

            showLevels = EditorGUILayout.Foldout(showLevels, "Slots (" + InitialItemDrop.allDropSlots.Count + ")");
            if (showLevels)
            {
                EditorGUI.indentLevel++;

                EditorGUI.indentLevel = 0;

                GUIStyle tableStyle = new GUIStyle("box");
                tableStyle.padding = new RectOffset(10, 10, 10, 10);
                tableStyle.margin.left = 32;

                GUIStyle headerColumnStyle = new GUIStyle();
                headerColumnStyle.fixedWidth = 35;

                GUIStyle columnStyle = new GUIStyle();
                columnStyle.fixedWidth = 100;

                GUIStyle rowStyle = new GUIStyle();
                rowStyle.fixedHeight = 25;

                GUIStyle rowHeaderStyle = new GUIStyle();
                rowHeaderStyle.fixedWidth = columnStyle.fixedWidth - 1;

                GUIStyle columnHeaderStyle = new GUIStyle();
                columnHeaderStyle.fixedWidth = 30;
                columnHeaderStyle.fixedHeight = 49.5f;

                GUIStyle columnLabelStyle = new GUIStyle();
                columnLabelStyle.fixedWidth = rowHeaderStyle.fixedWidth - 6;
                columnLabelStyle.alignment = TextAnchor.MiddleCenter;
                columnLabelStyle.fontStyle = FontStyle.Bold;

                GUIStyle cornerLabelStyle = new GUIStyle();
                cornerLabelStyle.fixedWidth = 50;
                cornerLabelStyle.alignment = TextAnchor.MiddleRight;
                cornerLabelStyle.fontStyle = FontStyle.BoldAndItalic;
                cornerLabelStyle.fontSize = 14;
                cornerLabelStyle.padding.top = -5;

                GUIStyle rowLabelStyle = new GUIStyle();
                rowLabelStyle.fixedWidth = 25;
                rowLabelStyle.alignment = TextAnchor.MiddleRight;
                rowLabelStyle.fontStyle = FontStyle.Bold;

                GUIStyle enumStyle = new GUIStyle("popup");
                rowStyle.fixedWidth = 65;

                EditorGUILayout.BeginHorizontal(tableStyle);
                for (int x = -1; x < InitialItemDrop.columns; x++)
                {
                    EditorGUILayout.BeginVertical((x == -1) ? headerColumnStyle : columnStyle);
                    for (int y = -1; y < InitialItemDrop.rows; y++)
                    {
                        if (x == -1 && y == -1)
                        {
                            EditorGUILayout.BeginVertical(rowHeaderStyle);
                            EditorGUILayout.LabelField("[X,Y]", cornerLabelStyle);
                            EditorGUILayout.EndHorizontal();
                        }
                        else if (x == -1)
                        {
                            EditorGUILayout.BeginVertical(columnHeaderStyle);
                            EditorGUILayout.LabelField(y.ToString(), rowLabelStyle);
                            EditorGUILayout.EndHorizontal();
                        }
                        else if (y == -1)
                        {
                            EditorGUILayout.BeginVertical(rowHeaderStyle);
                            EditorGUILayout.LabelField(x.ToString(), columnLabelStyle);
                            EditorGUILayout.EndHorizontal();
                        }

                        if (x >= 0 && y >= 0)
                        {
                            EditorGUILayout.BeginHorizontal(rowStyle);
                            int i = 0;
                            if (y == 0)
                            {
                                i = x;
                            }
                            else
                            {
                                i = InitialItemDrop.columns * y + x;
                            }

                            if (i >= InitialItemDrop.allDropSlots.Count)
                            {
                                InitialItemDrop.allDropSlots.Add(new ItemDropSlot());
                            }

                            InitialItemDrop.allDropSlots[i].mergeItem = 
                                (MergeItem)EditorGUILayout.ObjectField(
                                    InitialItemDrop.allDropSlots[i].mergeItem, 
                                    typeof(MergeItem), 
                                    allowSceneObjects: true, 
                                    GUILayout.Width(90));

                            EditorGUILayout.EndHorizontal();

                            InitialItemDrop.allDropSlots[i].slotState = 
                                (SlotState)EditorGUILayout.EnumPopup(
                                    InitialItemDrop.allDropSlots[i].slotState, 
                                    enumStyle);
                        }

                    }
                    EditorGUILayout.EndVertical();
                }
                EditorGUILayout.EndHorizontal();
            }
        }
    }
}

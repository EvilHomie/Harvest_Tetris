#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace Generator
{
    [CustomEditor(typeof(ItemGenerator))]
    public class ShapeDataEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            ItemGenerator shapeGen = (ItemGenerator)target;

            shapeGen.Width = EditorGUILayout.IntField("Width", shapeGen.Width);
            shapeGen.Height = EditorGUILayout.IntField("Height", shapeGen.Height);

            if (shapeGen.Cells == null || shapeGen.Cells.Length != shapeGen.Width * shapeGen.Height)
            {
                shapeGen.Cells = new bool[shapeGen.Width * shapeGen.Height];
            }

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Shape Grid:");
            for (int y = 0; y < shapeGen.Height; y++)
            {
                EditorGUILayout.BeginHorizontal();
                for (int x = 0; x < shapeGen.Width; x++)
                {
                    int index = y * shapeGen.Width + x;
                    shapeGen.Cells[index] = EditorGUILayout.Toggle(shapeGen.Cells[index], GUILayout.Width(20));
                }
                EditorGUILayout.EndHorizontal();
            }

            if (GUI.changed)
            {
                EditorUtility.SetDirty(shapeGen);
                shapeGen.Create();
            }
        }
    }
}
#endif
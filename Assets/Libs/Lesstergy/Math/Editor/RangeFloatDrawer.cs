using Lesstergy.Math;
using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(RangeFloat))]
public class RangeFloatDrawer : PropertyDrawer {

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
        label = EditorGUI.BeginProperty(position, label, property);
        Rect contentPosition = EditorGUI.PrefixLabel(position, label);
        contentPosition.width /= 2f;
        EditorGUI.indentLevel = 0;
        EditorGUIUtility.labelWidth = 30f;
        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("min"), new GUIContent("Min"));

        contentPosition.x += contentPosition.width;
        EditorGUIUtility.labelWidth = 30f;
        EditorGUI.PropertyField(contentPosition, property.FindPropertyRelative("max"), new GUIContent("Max"));
        EditorGUI.EndProperty();
    }
}

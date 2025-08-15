#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SliderBar))]
public class SliderBarEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // 1. Vẽ tất cả field mặc định (bao gồm fillTF)
        DrawDefaultInspector();

        // 2. Thêm slider tuỳ chỉnh phía dưới
        SliderBar slider = (SliderBar)target;
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();
        EditorGUILayout.LabelField("Current Value", GUILayout.Width(100));
        slider.currentValue = EditorGUILayout.Slider(
            slider.currentValue,
            slider.minValue,
            slider.maxValue
        );
        EditorGUILayout.EndHorizontal();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(slider);
            slider?.Fill();
        }
    }
}
#endif
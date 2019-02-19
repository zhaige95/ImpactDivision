using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(UITextData))]
public class TextDataEditor : UIDataEditor
{
    //private SerializedObject test;//序列化
    //private SerializedProperty dataType, stringData, floatData;//定义类型，变量a，变量b
    //void OnEnable()
    //{
    //    test = new SerializedObject(target);
    //    dataType = test.FindProperty("dataType");
    //    stringData = test.FindProperty("stringData");
    //    floatData = test.FindProperty("floatData");
    //}
    //public override void OnInspectorGUI()
    //{
    //    test.Update();//更新test
    //    EditorGUILayout.PropertyField(dataType);
    //    if (dataType.enumValueIndex == 0)
    //    {//当选择第一个枚举类型
    //        EditorGUILayout.PropertyField(stringData);
    //    }
    //    else if (dataType.enumValueIndex == 1)
    //    {
    //        EditorGUILayout.PropertyField(floatData);
    //    }
    //    test.ApplyModifiedProperties();//应用
    //}
}

using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
public class BoolTrigger
{

    public bool Value
    {
        get
        {
            var res = state;
            state = false;
            return res;
        }
        set
        {
            state = value;
        }
    }

    [SerializeField]
    bool state = false;

}

#if UNITY_EDITOR
[CustomPropertyDrawer(typeof(BoolTrigger))]
class BoolTriggerCustomDrawer : PropertyDrawer
{

    // Draw the property inside the given rect
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // Using BeginProperty / EndProperty on the parent property means that
        // prefab override logic works on the entire property.
        EditorGUI.BeginProperty(position, label, property);

        var targetObject = property.serializedObject.targetObject;
        var targetObjectClassType = targetObject.GetType();
        var field = targetObjectClassType.GetField(property.propertyPath);
        if (field != null)
        {
            var obj = field.GetValue(targetObject) as BoolTrigger;
            var res = EditorGUI.Toggle(position, property.name, obj.Value);
            obj.Value = res;
        }

        EditorGUI.EndProperty();
    }
}
#endif

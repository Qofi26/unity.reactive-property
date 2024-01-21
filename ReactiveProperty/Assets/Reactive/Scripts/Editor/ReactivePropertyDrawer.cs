using UnityEditor;
using UnityEngine;

namespace Reactive.Editor
{
    [CustomPropertyDrawer(typeof(IReactiveProperty<>), true)]
    public class ReactivePropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginChangeCheck();

            EditorGUI.PropertyField(position, property.FindPropertyRelative("_value"), label, true);

            if (EditorGUI.EndChangeCheck())
            {
                var targetObject = property.serializedObject.targetObject;
                var reactiveProperty = fieldInfo.GetValue(targetObject) as INotifiedReactiveProperty;
                reactiveProperty?.Notify();
            }
        }
    }
}
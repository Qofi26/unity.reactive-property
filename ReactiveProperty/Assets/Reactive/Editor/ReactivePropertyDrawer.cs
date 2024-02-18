using System;
using System.Reflection;
using System.Text.RegularExpressions;
using UnityEditor;
using UnityEngine;

namespace Erem.Reactive.Editor
{
    [CustomPropertyDrawer(typeof(IReactiveProperty<>), true)]
    public class ReactivePropertyDrawer : PropertyDrawer
    {
        private INotifiedReactiveProperty _notifiedReactiveProperty;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            const string fieldName = "_value";

            EditorGUI.BeginChangeCheck();

            EditorGUI.PropertyField(position, property.FindPropertyRelative(fieldName), label, true);

            if (!EditorGUI.EndChangeCheck())
            {
                return;
            }

            property.serializedObject.ApplyModifiedProperties();

            if (_notifiedReactiveProperty == null)
            {
                var target = property.serializedObject.targetObject;
                var paths = property.propertyPath.Split('.');

                var targetProp = paths.Length == 1
                    ? fieldInfo.GetValue(target)
                    : GetValueRecursive(target, 0, paths);

                _notifiedReactiveProperty = targetProp as INotifiedReactiveProperty;
            }

            _notifiedReactiveProperty?.Notify();
        }

        private object GetValueRecursive(object obj, int index, string[] paths)
        {
            var path = paths[index];
            var type = obj.GetType();

            FieldInfo field = null;
            while (field == null)
            {
                // attempt to get information about the field
                field = type.GetField(path,
                    BindingFlags.IgnoreCase
                    | BindingFlags.GetField
                    | BindingFlags.Instance
                    | BindingFlags.Public
                    | BindingFlags.NonPublic);

                if (type.BaseType == null
                    || type.BaseType.IsSubclassOf(typeof(IReactiveProperty<>)))
                {
                    break;
                }

                // if the field information is missing, it may be in the base class
                type = type.BaseType;
            }

            // If array, path = Array.data[index]
            if (field == null && path == "Array")
            {
                try
                {
                    path = paths[++index];
                    var m = Regex.Match(path, @"(.+)\[([0-9]+)*\]");
                    var arrayIndex = int.Parse(m.Groups[2].Value);
                    var arrayValue = (obj as System.Collections.IList)?[arrayIndex];
                    return index < paths.Length - 1 ? GetValueRecursive(arrayValue, ++index, paths) : arrayValue;
                }
                catch
                {
                    Debug.Log("ReactivePropertyDrawer Exception, objType:" + obj.GetType().Name + " path:" +
                              string.Join(", ", paths));
                    throw;
                }
            }

            if (field == null)
            {
                throw new Exception("Can't decode path" + string.Join(", ", paths));
            }

            var target = field.GetValue(obj);
            if (index < paths.Length - 1)
            {
                return GetValueRecursive(target, ++index, paths);
            }

            return target;
        }
    }
}
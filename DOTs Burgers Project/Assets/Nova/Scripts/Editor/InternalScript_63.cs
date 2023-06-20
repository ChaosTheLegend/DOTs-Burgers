using Nova.InternalNamespace_0.InternalNamespace_7;
using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Nova.InternalNamespace_17.InternalNamespace_18
{
    [CustomPropertyDrawer(typeof(InternalType_215))]
    internal class InternalType_579 : PropertyDrawer
    {
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private static readonly GUIContent InternalField_3712 = EditorGUIUtility.TrTextContent("\u2014", "Mixed Values");

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private static readonly Vector2 InternalField_3713 = new Vector2(230, 315);

        private struct InternalType_580
        {
            [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
            public Type InternalField_2602;
            [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
            public SerializedProperty InternalField_2603;
        }

        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private static SerializedProperty InternalField_2601 = null;
        [System.Diagnostics.DebuggerBrowsable(System.Diagnostics.DebuggerBrowsableState.Never)]
        private static InternalType_581 InternalField_3311 = null;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return InternalMethod_173(property) ? EditorGUIUtility.singleLineHeight : EditorGUI.GetPropertyHeight(property, true);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            InternalMethod_3314(position, property, label);
        }

        public static void InternalMethod_3314(Rect InternalParameter_3447, SerializedProperty InternalParameter_3448, GUIContent InternalParameter_3449)
        {
            InternalParameter_3449 = EditorGUI.BeginProperty(InternalParameter_3447, InternalParameter_3449, InternalParameter_3448);
            InternalType_579.InternalField_2601 = InternalParameter_3448;

            bool InternalVar_1 = EditorGUI.showMixedValue;
            bool InternalVar_2 = InternalMethod_173(InternalParameter_3448);

            if (InternalVar_2)
            {
                EditorGUI.PrefixLabel(InternalParameter_3447, InternalParameter_3449);
            }

            EditorGUI.showMixedValue = InternalVar_2;
            InternalMethod_2345(InternalParameter_3447, InternalParameter_3449);
            EditorGUI.showMixedValue = InternalVar_1;

            if (!InternalVar_2)
            {
                EditorGUI.PropertyField(InternalParameter_3447, InternalParameter_3448, includeChildren: true);
            }

            EditorGUI.EndProperty();
        }

        private static void InternalMethod_2345(Rect InternalParameter_2757, GUIContent InternalParameter_2758)
        {
            Rect InternalVar_1 = InternalParameter_2757;

            float InternalVar_2 = Mathf.Max(EditorStyles.boldLabel.CalcSize(InternalParameter_2758).x, InternalType_573.InternalProperty_472);

            InternalVar_1.x += InternalVar_2 + InternalType_573.InternalField_2557;
            InternalVar_1.width = InternalParameter_2757.width - InternalVar_2 - InternalType_573.InternalField_2557;
            InternalVar_1.height = EditorGUIUtility.singleLineHeight;

            using (new EditorGUI.IndentLevelScope(-EditorGUI.indentLevel))
            {
                Type InternalVar_3 = InternalMethod_2351(InternalField_2601.managedReferenceFullTypename); ;


                bool InternalVar_4 = InternalVar_3 != null;
                string InternalVar_5 = InternalVar_4 ? InternalVar_3.Name : "None (Unassigned)";
                string InternalVar_6 = InternalVar_4 ? InternalVar_3.Assembly.FullName : "Null";

                string InternalVar_7 = ObjectNames.NicifyVariableName(InternalVar_5.Substring(InternalVar_5.LastIndexOf(".") + 1));

                if (InternalVar_3 != null)
                {
                    TypeMenuNameAttribute InternalVar_8 = InternalVar_3.GetCustomAttribute<TypeMenuNameAttribute>();

                    if (InternalVar_8 != null && !string.IsNullOrWhiteSpace(InternalVar_8.DisplayName))
                    {
                        InternalVar_7 = InternalVar_8.DisplayName;
                    }
                }

                GUIContent InternalVar_9 = EditorGUI.showMixedValue ? InternalField_3712 : new GUIContent(InternalVar_7, InternalVar_5 + " (" + InternalVar_6 + ")");

                bool InternalVar_10 = GUI.Button(InternalVar_1, InternalVar_9, EditorStyles.popup);

                if (!InternalVar_10)
                {
                    return;
                }

                if (InternalField_3311 == null)
                {
                    Type InternalVar_11 = InternalMethod_2351(InternalField_2601.managedReferenceFieldTypename);
                    if (InternalVar_11 == null)
                    {
                        Debug.LogError($"SerializeReference type, [{InternalField_2601.managedReferenceFieldTypename}], not found.");
                        return;
                    }

                    InternalField_3311 = new InternalType_581(InternalVar_11, "Visuals");
                    InternalField_3311.InternalEvent_6 += InternalMethod_566;
                }

                InternalField_3311.InternalProperty_1168 = InternalField_3713;
                InternalField_3311.Show(InternalVar_1);
            }
        }

        private static void InternalMethod_566(Type InternalParameter_79)
        {
            if (InternalParameter_79 == null)
            {
                InternalField_2601.managedReferenceValue = null;
                InternalField_2601.serializedObject.ApplyModifiedProperties();
                return;
            }

            UnityEngine.Object[] InternalVar_1 = InternalField_2601.serializedObject.targetObjects;

            if (InternalVar_1 == null || InternalVar_1.Length == 0)
            {
                InternalVar_1 = new UnityEngine.Object[1] { InternalField_2601.serializedObject.targetObject };
            }

            foreach (UnityEngine.Object InternalVar_2 in InternalVar_1)
            {
                SerializedObject InternalVar_3 = new SerializedObject(InternalVar_2);
                SerializedProperty InternalVar_4 = InternalVar_3.FindProperty(InternalField_2601.propertyPath);

                if (InternalMethod_2351(InternalVar_4.managedReferenceFullTypename) != InternalParameter_79)
                {
                    InternalVar_4.managedReferenceValue = Activator.CreateInstance(InternalParameter_79);
                    InternalVar_4.serializedObject.ApplyModifiedProperties();
                }
            }
        }

        public static Type InternalMethod_2351(string InternalParameter_2765)
        {
            (string InternalVar_1, string InternalVar_2) = InternalMethod_2352(InternalParameter_2765);

            return Type.GetType($"{InternalVar_2}, {InternalVar_1}");
        }

        public static (string AssemblyName, string ClassName) InternalMethod_2352(string InternalParameter_2766)
        {
            if (string.IsNullOrEmpty(InternalParameter_2766))
            {
                return ("", "");
            }

            string[] InternalVar_1 = InternalParameter_2766.Split(char.Parse(" "));
            string InternalVar_2 = InternalVar_1[1];
            string InternalVar_3 = InternalVar_1[0];
            return (InternalVar_3, InternalVar_2);
        }

        public static bool InternalMethod_173(SerializedProperty InternalParameter_2622)
        {
            UnityEngine.Object[] InternalVar_1 = InternalParameter_2622.serializedObject.targetObjects;

            if (InternalVar_1 == null || InternalVar_1.Length < 2)
            {
                return false;
            }

            string InternalVar_2 = InternalParameter_2622.managedReferenceFullTypename;

            foreach (UnityEngine.Object InternalVar_3 in InternalVar_1)
            {
                SerializedObject InternalVar_4 = new SerializedObject(InternalVar_3);
                SerializedProperty InternalVar_5 = InternalVar_4.FindProperty(InternalParameter_2622.propertyPath);

                if (InternalVar_2 != InternalVar_5.managedReferenceFullTypename)
                {
                    return true;
                }
            }

            return false;
        }
    }
}

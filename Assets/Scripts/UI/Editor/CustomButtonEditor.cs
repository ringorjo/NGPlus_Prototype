using UnityEditor;
using UnityEditor.UI;


namespace BGNS_Studios
{
    [CustomEditor(typeof(CustomButton), true)]
    public class CustomButtonEditor : ButtonEditor
    {
        SerializedProperty buttonTextProp;
        SerializedProperty stylesProp;

        protected override void OnEnable()
        {
            base.OnEnable();

            buttonTextProp = serializedObject.FindProperty("_buttonText");
            stylesProp = serializedObject.FindProperty("_states");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            serializedObject.Update();

            EditorGUILayout.Space();
            EditorGUILayout.PropertyField(buttonTextProp);
            EditorGUILayout.PropertyField(stylesProp, true);

            serializedObject.ApplyModifiedProperties();
        }
    }
}


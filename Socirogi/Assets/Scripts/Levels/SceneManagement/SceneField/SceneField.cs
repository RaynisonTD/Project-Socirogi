using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Levels.SceneManagement.SceneField
{
    [System.Serializable]
    public class SceneField
    {
#if UNITY_EDITOR
        [SerializeField] private SceneAsset sceneAsset;  // Editor-only scene reference
#endif

        [SerializeField] private string sceneName = "";   // Serialized name used at runtime

        public string SceneName => sceneName;

        // Implicit conversion to string for convenience
        public static implicit operator string(SceneField sceneField)
        {
            return sceneField.sceneName;
        }
    }

#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(SceneField))]
    public class SceneFieldPropertyDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            SerializedProperty sceneAssetProp = property.FindPropertyRelative("sceneAsset");
            SerializedProperty sceneNameProp = property.FindPropertyRelative("sceneName");

            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            if (sceneAssetProp != null)
            {
                EditorGUI.BeginChangeCheck();
                var newScene = EditorGUI.ObjectField(position, sceneAssetProp.objectReferenceValue, typeof(SceneAsset), false);
                if (EditorGUI.EndChangeCheck())
                {
                    sceneAssetProp.objectReferenceValue = newScene;
                    sceneNameProp.stringValue = newScene != null ? (newScene as SceneAsset).name : "";
                }
            }

            EditorGUI.EndProperty();
        }
    }
#endif
}


namespace Levels.SceneManagement.SceneField
{
#if UNITY_EDITOR
    using UnityEditor;
    using UnityEngine;

    [CustomEditor(typeof(LevelChangeTrigger))]
    public class LevelChangeTriggerEditor : Editor
    {
        
        // Function to hide SceneToLoad property in editor if Random level switch fucntion is being used
        public override void OnInspectorGUI()
        {
            serializedObject.Update();

            SerializedProperty useRandomSceneProp = serializedObject.FindProperty("useRandomSceneFromLevelManager");
            SerializedProperty sceneToLoadProp = serializedObject.FindProperty("sceneToLoad");

            if (useRandomSceneProp != null)
            {
                EditorGUILayout.PropertyField(useRandomSceneProp);

                if (!useRandomSceneProp.boolValue)
                {
                    if (sceneToLoadProp != null)
                    {
                        EditorGUILayout.PropertyField(sceneToLoadProp);
                    }
                    else
                    {
                        EditorGUILayout.HelpBox("Could not find 'sceneToLoad' property!", MessageType.Error);
                    }
                }
                else
                {
                    EditorGUILayout.HelpBox("SceneToLoad is hidden because LevelManager loads random scenes.", MessageType.Info);
                }
            }
            else
            {
                EditorGUILayout.HelpBox("Could not find 'useRandomSceneFromLevelManager' property!", MessageType.Error);
            }

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}

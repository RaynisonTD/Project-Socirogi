using UnityEngine;

namespace Levels.SceneManagement.SceneInformation
{
    [CreateAssetMenu(fileName = "NewSceneInfo", menuName = "Scenes/SceneInfo")]
    public class SceneInfo : ScriptableObject
    {
        public SceneField.SceneField scene;      // Your SceneField reference
        public string defaultSpawnID; // The spawn point ID to use by default
    }
}
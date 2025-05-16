using System.Collections.Generic;
using Levels.SceneManagement.SceneField;
using UnityEngine;

public class LevelChangeTrigger : MonoBehaviour
{
    [Header("Scene to Load")]
    [SerializeField] private SceneField sceneToLoad;
    
    [SerializeField]
    private bool useRandomSceneFromLevelManager;

    
    
   

    [Tooltip("Only objects with this tag will trigger the scene change")]
    public string playerTag = "Player";

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            

            // Load the random scene (or specific one if you want)
            LevelManager.instance.TransitionToRandomScene("CrossFade");
        }
    }
}
    
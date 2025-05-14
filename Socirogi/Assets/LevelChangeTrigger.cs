using System.Collections.Generic;
using UnityEngine;

public class LevelChangeTrigger : MonoBehaviour
{

   

    // The tag assigned to the player character 
    [Tooltip("Only objects with this tag will trigger the scene change")]
    public string playerTag = "Player";

    
    private void OnTriggerEnter(Collider other)
    {
        // If the player makes contact with the collider, trigger a scene change
        if (other.CompareTag(playerTag))
        {
            LevelManager.instance.TransitionToScene("Level 1","CrossFade");
        }
    }
}
    
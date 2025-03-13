using System;
using UnityEngine;
using UnityEngine.UIElements;

public class GameEventsManager : MonoBehaviour
{
    public static GameEventsManager instance { get; private set; }
    public DialogueEvents dialogueEvents;
    public InputEvents inputEvents;
    
    private void Awake()
        {
            if (instance != null)
            {
                Debug.LogError("er is meer dan 1 game event manager gevonden");
            }

            instance = this;
            
            //begin all events
            dialogueEvents = new DialogueEvents();
            inputEvents = new InputEvents();
        }
}

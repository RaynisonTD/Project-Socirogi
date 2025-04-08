using System;
using UnityEngine;

public class DialogueEvents
{
    // Event triggered when entering a dialogue with a specific knot.
    public event Action<string> onEnterDialogue;
    
    public void EnterDialogue(string knotName)
    {
        Debug.Log("Dialogue Entered");
        onEnterDialogue?.Invoke(knotName);
    }

    // Event triggered when a dialogue starts.
    public event Action onDialogStarted;
    
    public void DialogStarted()
    {
        if(onDialogStarted != null)
        {
            Debug.Log("dialoog gestart");
            onDialogStarted();
        }
    }
    
    // Event triggered when a dialogue finishes.
    public event Action onDialogFinished;
    
    public void DialogFinished()
    {
        if (onDialogFinished != null)
        {
            Debug.Log("dialoog gefinishd");
            onDialogFinished();
        }
    }
    
    // Event triggered when a dialogue line is displayed.
    public event Action<string> onDisplayDialogue;
    
    public void DisplayDialogue(string dialogueLine)
    {
        onDisplayDialogue?.Invoke(dialogueLine);
    }
}
using System;
using UnityEngine;

public class DialogueEvents
{
    public event Action<string>  onEnterDialogue;
    public void EnterDialogue(string knotName)
    {
        Debug.Log("Dialogue Entered");
       onEnterDialogue?.Invoke(knotName);
    }


    public event Action onDialogStarted;

    public void DialogStarted()
    {
        if(onDialogStarted != null)
        {
            Debug.Log("dialoog gestart");
            onDialogStarted();
        }
    }
    
    public event Action onDialogFinished;

    public void DialogFinished()
    {
        if (onDialogFinished != null)
        {
            Debug.Log("dialoog gefinishd");
            onDialogFinished();
        }
    }
    
    public event Action<string> onDisplayDialogue;

    public void DisplayDialogue(string dialogueLine)
    {
        onDisplayDialogue?.Invoke(dialogueLine);
    }

}

using System;
public class DialogueEvents
{
    public event Action<string>  onEnterDialogue;
    public void EnterDialogue(string knotName)
    {
       onEnterDialogue?.Invoke(knotName);
    }


    public event Action onDialogStarted;

    public void DialogStarted()
    {
        if(onDialogStarted != null)
        {
            onDialogStarted();
        }
    }
    
    public event Action onDialogFinished;

    public void DialogFinished()
    {
        if (onDialogFinished != null)
        {
            onDialogFinished();
        }
    }
    
    public event Action<string> onDisplayDialogue;

    public void DisplayDialogue(string dialogueLine)
    {
        if (onDisplayDialogue != null)
        {
            onDisplayDialogue(dialogueLine);
        }
    }

}

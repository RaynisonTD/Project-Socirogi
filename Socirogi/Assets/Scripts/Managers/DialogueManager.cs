using UnityEngine;
using Ink.Runtime;
using System.Collections.Generic;
public class DialogueManager : MonoBehaviour
{
    [Header("Ink Story")] [SerializeField] private TextAsset inkJson;
    
    
    
    private Story story;
    
    private bool dialoguePlaying = false;

    private void Awake()
    {
        story  = new Story(inkJson.text);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnEnable()
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue += EnterDialogue;
        GameEventsManager.instance.inputEvents.onSubmitPressed += SubmitPressed;
    }
    private void OnDisable()
    {
        GameEventsManager.instance.dialogueEvents.onEnterDialogue -= EnterDialogue;
        GameEventsManager.instance.inputEvents.onSubmitPressed -= SubmitPressed;
    }
    private void SubmitPressed(InputEventContext context)
    {
        Debug.Log("SubmitPressed event fired!"); // Controleer of dit in de console komt
    
        if (!dialoguePlaying)
        {
            Debug.Log("Dialogue is not playing, ignoring submit.");
            return;
        }
    
        ContinueOrExitStory();
    }


    
    private void EnterDialogue(string knotName)
    {
        if (dialoguePlaying)
        {
            return;
        }
        dialoguePlaying = true;
        GameEventsManager.instance.dialogueEvents.DialogStarted();

        if (!knotName.Equals(""))
        {
            story.ChoosePathString(knotName);
        }
        else
        {
            Debug.LogWarning("know naam was een emtpy string ofzo iets");
        }

        ContinueOrExitStory();
    }

    private void ContinueOrExitStory()
    {
        if (story.canContinue)
        {
            string dialogueLine = story.Continue(); 
            GameEventsManager.instance.dialogueEvents.DisplayDialogue(dialogueLine);
        }
        else
        {
            ExitDialogue();
        }
    }

    private void ExitDialogue()
    {
        GameEventsManager.instance.dialogueEvents.DialogFinished();
        dialoguePlaying = false;
        
        
        story.ResetState();
    }

    //Skip function.
    public void SkipDialogue()
    {
        GameEventsManager.instance.dialogueEvents.DialogFinished();
        dialoguePlaying = false;
        
        
        story.ResetState();
    }
    
    //Manual next line button.
    public void NextLine()
    {
        if (story.canContinue)
        {
            string dialogueLine = story.Continue();
            GameEventsManager.instance.dialogueEvents.DisplayDialogue(dialogueLine);
        }
        else
        {
            ExitDialogue();
        }
    }
}

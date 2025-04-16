using System.Net.Mime;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;

public class Interactable : MonoBehaviour
{
    private bool isPlayerNearby = false;

    [Header("Dialogue (optional)")] [SerializeField]
    private string dialogueKnotName;
    //speler dichtbij variabele
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }

    void Update()
    {
        //check if the player is nearby
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("binnen de border");
            if (!dialogueKnotName.Equals(""))
            {
                //start dialogue script
                GameEventsManager.instance.dialogueEvents.EnterDialogue(dialogueKnotName);
                Debug.Log("Dialogue Entered");
            }
        }
    }
}
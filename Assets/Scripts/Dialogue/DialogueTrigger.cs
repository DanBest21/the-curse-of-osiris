using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : IInteractable
{
    public Dialogue dialogue;
    //public string description;

    public void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
    
    public override void Interact()
    {
        handler.DisableInteractables();
        handler.RemoveInteractable(gameObject.transform);
        TriggerDialogue();
    }

}

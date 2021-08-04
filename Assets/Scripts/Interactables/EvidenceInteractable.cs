using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvidenceInteractable : IInteractable
{

    public Dialogue dialogue;
    public Evidence evidence;

    public override void Interact()
    {
        if(evidence != null)
        {
            handler.manager.CollectEvidence(evidence);
        }
        handler.DisableInteractables();
        TriggerDialogue();
    }

    public virtual void TriggerDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }

    
}

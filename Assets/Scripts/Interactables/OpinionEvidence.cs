using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpinionEvidence : EvidenceInteractable
{
    public Dialogue belowThreshold;
    public string charName;
    public float threshold;

    public override void TriggerDialogue()
    {
        Character c = handler.manager.GetCharacter(charName);
        if (c.Opinion() >= threshold)
        {
            FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        }
        else
        {
            FindObjectOfType<DialogueManager>().StartDialogue(belowThreshold);
        }
    }
}

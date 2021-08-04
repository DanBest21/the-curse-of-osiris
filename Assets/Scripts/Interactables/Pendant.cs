using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pendant : EvidenceInteractable
{

    public string charName;
    public float opinionChange;

    public override void TriggerDialogue()
    {
        handler.manager.GetCharacter(charName).ChangeOpinion(opinionChange);

        base.TriggerDialogue();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassageInteractable : EvidenceInteractable
{
    public GameObject passage;
    public GameObject magazines;
    private bool init;

    public override void TriggerDialogue()
    {
        base.TriggerDialogue();
        handler.DisableInteractables();
        passage.SetActive(false);
        magazines.SetActive(true);
        handler.RemoveInteractable(gameObject.transform);
    }

    public override void Interact()
    {
        if (init)
        {
            if (evidence != null)
            {
                handler.manager.CollectEvidence(evidence);
            }
            TriggerDialogue();
            init = false;
        }
    }

    private void Start()
    {
        init = true;
        magazines.SetActive(false);
    }
}

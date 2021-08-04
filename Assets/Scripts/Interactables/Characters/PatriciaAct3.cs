using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatriciaAct3 : IInteractable
{

    public Dialogue unlockStudy;
    public Dialogue requestPendant;

    public float opinionThresholdBase;
    public string charName;

    public Dialogue aboveThresholdB;
    public Dialogue belowThresholdB;

    private bool init;

    public override void Interact()
    {
        
        if (init)
        {
            bool bedoom = false;
            bool pendant = false;

            List<Evidence> evi = handler.manager.CollectedEvidence;
            foreach (Evidence evidence in evi)
            {
                if (evidence.Name().Equals("Unlock Bedroom")) bedoom = true;
                else if (evidence.Name().Equals("Pendant")) pendant = true;
            }

            handler.DisableInteractables();
            if (!bedoom)
            {
                BeginDialogue(unlockStudy);
            }
            else if (!pendant)
            {
                BeginDialogue(requestPendant);
            }
            else
            {
                float opinion = handler.manager.GetCharacter(charName).Opinion();
                handler.RemoveInteractable(gameObject.transform);
                if (opinion >= opinionThresholdBase)
                {
                    BeginDialogue(aboveThresholdB);
                }
                else
                {
                    BeginDialogue(belowThresholdB);
                }
                init = false;
            }
        }
        
    }

    public void BeginDialogue(Dialogue d)
    {
        FindObjectOfType<DialogueManager>().StartDialogue(d);
    }

    // Start is called before the first frame update
    void Start()
    {
        init = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

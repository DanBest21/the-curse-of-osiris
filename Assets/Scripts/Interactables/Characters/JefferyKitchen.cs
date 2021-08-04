using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefferyKitchen : IInteractable
{

    public float opinionThresholdBase;
    public string charName;

    public Dialogue aboveThresholdB;
    public Dialogue belowThresholdB;


    private bool init;

    public override void Interact()
    {
        if (init)
        {
            float opinion = handler.manager.GetCharacter(charName).Opinion();
            handler.DisableInteractables();
            handler.RemoveInteractable(gameObject.transform);
            if (opinion >= opinionThresholdBase)
            {
                FindObjectOfType<DialogueManager>().StartDialogue(aboveThresholdB);
            }
            else
            {
                FindObjectOfType<DialogueManager>().StartDialogue(belowThresholdB);
            }
            init = false;
        }
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

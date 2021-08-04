using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JefferyDining : IInteractable
{

    public Dialogue dialogue;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Interact()
    {
        handler.DisableInteractables();
        handler.RemoveInteractable(gameObject.transform);
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}

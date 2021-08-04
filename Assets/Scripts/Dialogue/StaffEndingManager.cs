using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StaffEndingManager : DialogueManager
{
    public string staffEndingScene;
    private Evidence evidence;

    public override void StartDialogue(Dialogue dialogue)
    {
        base.StartDialogue(dialogue);
        evidence = dialogue.evidence;
        Debug.Log("Staff");
    }

    protected override void EndDialogue()
    {
        if (evidence.Name().Equals("StaffEnding"))
        {
            interactHandler.manager.NextAct();
            interactHandler.manager.SaveGame();
            Debug.Log("Starting " + interactHandler.manager.CurrentAct);
            SceneManager.LoadScene(staffEndingScene);
        }
        else
        {
            base.EndDialogue();
        }
        
    }
}

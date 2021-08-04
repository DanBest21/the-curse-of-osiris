using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ProDiaManager : DialogueManager
{
    public string scene;
    public bool nextAct = true;

    protected override void EndDialogue()
    {
        if (nextAct)
        {
            interactHandler.manager.NextAct();
        }
        interactHandler.manager.SaveGame();
        Debug.Log("Ending prologue");
        SceneManager.LoadScene(scene);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableHandler : MonoBehaviour
{
    public GameManager manager;
    public List<Transform> interactables;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void DisableInteractables()
    {
        foreach (Transform t in interactables)
        {

            t.GetComponent<IInteractable>().enabled = false;
            t.GetComponent<Collider2D>().enabled = false;
        }
    }

    public void EnableInteractables()
    {
        foreach (Transform t in interactables)
        {
            t.GetComponent<IInteractable>().enabled = true;
            t.GetComponent<Collider2D>().enabled = true;
        }
    }

    public void CollectEvidence(Evidence evidence)
    {
        manager.CollectEvidence(evidence);
    }

    public Act CurrentAct()
    {
        return manager.CurrentAct;
    }

    public void AddInteractables(Transform interactable)
    {
        interactables.Add(interactable);
    }

    public void RemoveInteractable(Transform interactable)
    {
        interactables.Remove(interactable);
    }


}

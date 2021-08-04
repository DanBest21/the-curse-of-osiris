using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class IInteractable : MonoBehaviour
{
    public string description;
    public InteractableHandler handler;

    public abstract void Interact();
}

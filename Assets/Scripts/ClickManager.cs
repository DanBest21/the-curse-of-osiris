using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using Pathfinding;

public class ClickManager : MonoBehaviour
{
    private Vector2 mousePos;
    private RaycastHit2D hit;
    private GameObject selection;
    private GameObject interactableObj;
    private bool moving;

    private Transform prevInteract;
    private Transform newInteract;

    private Texture2D normalCursor;
    private Texture2D speakCursor;
    private Texture2D investigateCursor;

    public TextMeshProUGUI highlight;

    // Start is called before the first frame update
    void Start()
    {
        highlight.text = "";
        moving = false;
        prevInteract = null;

        normalCursor = Resources.Load<Texture2D>("Cursors/Normal");
        speakCursor = Resources.Load<Texture2D>("Cursors/Speak");
        investigateCursor = Resources.Load<Texture2D>("Cursors/Investigate");
    }

    private void Update()
    {

        if (moving)
        {
            if (gameObject.GetComponent<AIPath>().reachedDestination)
            {
                if (prevInteract == null || !prevInteract.Equals(newInteract))
                {
                    prevInteract = newInteract;

                }else if (prevInteract.Equals(newInteract))
                {
                    Debug.Log("Prev: Reached " + prevInteract.name);
                    interactableObj.GetComponent<IInteractable>().Interact();
                    GameManager.Instance.StopSFX();
                    moving = false;
                }
            }
        }

        mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        RaycastHit2D hover = Physics2D.Raycast(mousePos, Vector2.zero);


        if (hover.collider != null && hover.transform.CompareTag("Interactable"))
        {
            highlight.text = hover.transform.GetComponent<IInteractable>().description;

            if (highlight.text.ToLower().Contains("talk") || highlight.text.ToLower().Contains("speak"))
            {
                Cursor.SetCursor(speakCursor, Vector2.zero, CursorMode.Auto);
            }
            else
            {
                Cursor.SetCursor(investigateCursor, Vector2.zero, CursorMode.Auto);
            }

            selection = hover.collider.gameObject;
        }
        else if (selection != null)
        {
            highlight.text = "";
            Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.Auto);
            selection = null;
        }

    }


    void OnFire()
    {
        //Debug.Log("Mouse Click");
        //Debug.Log(mousePos);

        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);
        


        if(hit.collider != null)
        {
            if (hit.transform.CompareTag("Interactable"))
            {
                AIDestinationSetter ai = gameObject.GetComponent<AIDestinationSetter>();
                if(ai != null)
                {
                    ai.target = hit.transform;
                    Debug.Log("Moving to "+hit.transform.name);
                    moving = true;

                    GameManager.Instance.PlaySFX("Footsteps", 2, true);

                    interactableObj = hit.transform.gameObject;
                    newInteract = hit.transform;
                }
                else
                {
                    Debug.Log("fail");
                }

                
                //gameObject.GetComponent<AIDestinationSetter>().target = null;
            }
        }
    }

   
}

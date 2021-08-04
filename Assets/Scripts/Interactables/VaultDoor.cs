using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VaultDoor : IInteractable
{
    public GameObject staff;
    public GameObject vaultDoors;
    public Keypad keypad;

    private bool init;

    public override void Interact()
    {
        if (!GameManager.Instance.KeypadLocked())
        {
            Trigger();
            Debug.Log(GameManager.Instance.NumberOfKeypadAttempts);
        }
    }

    private void Trigger()
    {
        keypad.OpenKeypad();
        //vaultDoors.SetActive(false);
        //staff.SetActive(true);
        //init = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        init = true;
        staff.GetComponent<IInteractable>().enabled =false;
        staff.GetComponent<Collider2D>().enabled = false;
    }

    private void Update()
    {
        if (init)
        {
            if (GameManager.Instance.KeypadLocked())
            {
                description = "Locked Vault Door";
            }
        }
    }
}

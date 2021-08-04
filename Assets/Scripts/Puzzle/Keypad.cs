using System;
using UnityEngine;
using TMPro;

public class Keypad : MonoBehaviour
{
    [SerializeField]
    private GameObject keypadObject;
    [SerializeField]
    private TextMeshProUGUI[] digits;
    [SerializeField]
    private TextMeshProUGUI[] crosses;
    [SerializeField]
    private TextMeshProUGUI highlightText;

    public GameObject staff;
    public GameObject vaultDoors;
    public IInteractable vaultInteractable;

    public void Start()
    {
        //if (GameManager.Instance.KeypadLocked())
        //{
        //vaultInteractable.gameObject.SetActive(false);
        //return;
        //}

        foreach (TextMeshProUGUI digit in digits)
        {
            digit.text = "_";
        }

        foreach (TextMeshProUGUI cross in crosses)
        {
            cross.text = "";
        }

        keypadObject.SetActive(false);
    }

    public void OpenKeypad()
    {
        keypadObject.SetActive(true);
    }

    public void CloseKeypad()
    {
        keypadObject.SetActive(false);
    }

    public void PressDigit(int d)
    {
        bool success = false;
        
        foreach (TextMeshProUGUI digit in digits)
        {
            if (digit.text == "_")
            {
                digit.text = d.ToString();
                GameManager.Instance.PlaySFX("Button");
                success = true;
                break;
            }
        }

        if (!success)
            GameManager.Instance.PlaySFX("Error");
    }

    public void DeleteDigit()
    {
        TextMeshProUGUI[] reverseDigits = new TextMeshProUGUI[digits.Length];
        Array.Copy(digits, reverseDigits, digits.Length);
        Array.Reverse(reverseDigits);

        bool success = false;

        foreach (TextMeshProUGUI digit in reverseDigits)
        {
            if (digit.text != "_")
            {
                digit.text = "_";
                GameManager.Instance.PlaySFX("Back");
                success = true;
                break;
            }
        }

        if (!success)
            GameManager.Instance.PlaySFX("Error");
    }

    public void EnterCode()
    {
        string attempt = "";

        foreach (TextMeshProUGUI digit in digits)
        {
            attempt += digit.text;
        }

        bool lastAttempt = true;

        foreach (TextMeshProUGUI cross in crosses)
        {
            if (cross.text != "X")
            {
                lastAttempt = false;
                break;
            }
        }

        if (attempt == GameManager.PuzzleCode)
        {
            GameManager.Instance.PlaySFX("Correct");

            keypadObject.SetActive(false);

            vaultDoors.SetActive(false);
            staff.GetComponent<IInteractable>().enabled = true;
            staff.GetComponent<Collider2D>().enabled = true;
            vaultInteractable.gameObject.SetActive(false);

            highlightText.text = "Success! Door Unlocked!";
        }
        else if (lastAttempt)
        {
            GameManager.Instance.KeypadFailed();
            GameManager.Instance.PlaySFX("Wrong");

            keypadObject.SetActive(false);
            //vaultInteractable.gameObject.SetActive(false);

            highlightText.text = "Keypad Locked!";
        }
        else
        {
            GameManager.Instance.KeypadFailed();

            foreach (TextMeshProUGUI cross in crosses)
            {
                if (cross.text == "")
                {
                    cross.text = "X";
                    break;
                }
            }

            GameManager.Instance.PlaySFX("Wrong");
        }
    }
}

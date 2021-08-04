using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Journal : MonoBehaviour
{
    [SerializeField]
    private GameObject journalObject;
    [SerializeField]
    private GameObject evidenceMenu;
    [SerializeField]
    private GameObject left;
    [SerializeField]
    private GameObject right;
    [SerializeField]
    private GameObject back;
    [SerializeField]
    private TextMeshProUGUI title;
    [SerializeField]
    private GameObject room;
    [SerializeField]
    private GameObject description;
    private int pageNumber = 1;

    private const int itemsPerPage = 10;

    public void Start()
    {
        journalObject.SetActive(false);
    }

    public void ToggleJournal()
    {
        journalObject.SetActive(!journalObject.activeSelf);

        if (journalObject.activeSelf)
        {
            GameManager.Instance.PlaySFX("Open Book");
            pageNumber = 1;
            SetupMenu();
        }
        else
        {
            GameManager.Instance.PlaySFX("Close Book");
        }
    }

    public void CloseJournal()
    {
        journalObject.SetActive(false);
        GameManager.Instance.PlaySFX("Close Book");
    }

    public void NextPage()
    {
        pageNumber++;
        SetupMenu();
        GameManager.Instance.PlaySFX("Page");
    }

    public void PreviousPage()
    {
        pageNumber--;
        SetupMenu();
        GameManager.Instance.PlaySFX("Page");
    }

    public void Back()
    {
        SetupMenu();
        GameManager.Instance.PlaySFX("Zoom Out");
    }

    public void LoadEvidence(string evidenceName)
    {
        Evidence evidence = GameManager.Instance.CollectedEvidence.Find(e => e.Name() == evidenceName);

        left.SetActive(false);
        right.SetActive(false);
        evidenceMenu.SetActive(false);

        back.SetActive(true);
        room.SetActive(true);
        description.SetActive(true);

        title.text = evidence.Name();
        room.transform.GetComponent<TextMeshProUGUI>().text = "Found in the " + RoomUtils.GetDescription(evidence.Room()) + ".";
        description.transform.GetComponent<TextMeshProUGUI>().text = evidence.Description();

        GameManager.Instance.PlaySFX("Zoom In", 0.5f);
    }

    private void SetupMenu()
    {
        title.text = "Evidence";

        back.SetActive(false);
        room.SetActive(false);
        description.SetActive(false);

        List<Evidence> collectedEvidence = new List<Evidence>();
        List<Evidence> utilityEvidence = Resources.LoadAll<Evidence>("Evidence/Utility").ToList();

        foreach (Evidence evidence in GameManager.Instance.CollectedEvidence)
        {
            if (!utilityEvidence.Contains(evidence))
            {
                collectedEvidence.Add(evidence);
            }
        }

        Button[] buttons = evidenceMenu.transform.GetComponentsInChildren<Button>();

        bool reachedEnd = false;

        for (int i = (pageNumber - 1) * itemsPerPage; i < pageNumber * itemsPerPage; i++)
        {
            if (i >= collectedEvidence.Count)
            {
                reachedEnd = true;
            }

            int buttonIndex = i % itemsPerPage;

            if (!reachedEnd)
            {
                buttons[buttonIndex].interactable = true;
                string evidenceName = collectedEvidence[i].Name();
                buttons[buttonIndex].onClick.AddListener(delegate { LoadEvidence(evidenceName); });
                evidenceMenu.transform.GetChild(buttonIndex).GetComponentInChildren<TextMeshProUGUI>().text = evidenceName;
            }
            else
            {
                buttons[buttonIndex].interactable = false;
                evidenceMenu.transform.GetChild(buttonIndex).GetComponentInChildren<TextMeshProUGUI>().text = "";
            }
        }

        evidenceMenu.SetActive(true);

        if (pageNumber == 1 && collectedEvidence.Count > itemsPerPage)
        {
            left.SetActive(false);
            right.SetActive(true);
        }
        else if (pageNumber == 1)
        {
            left.SetActive(false);
            right.SetActive(false);
        }
        else if (collectedEvidence.Count <= itemsPerPage * pageNumber)
        {
            left.SetActive(true);
            right.SetActive(false);
        }
        else
        {
            left.SetActive(true);
            right.SetActive(true);
        }
    }
}

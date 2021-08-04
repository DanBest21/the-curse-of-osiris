using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Act3Inter : DialogueManager
{

    private List<string> suspects;

    protected override void EndDialogue()
    {
        //dialogueBox.SetActive(false);
        SetupOptions();
        SetupAccusation();
    }

    void SetupOptions()
    {
        bool answer1 = false;
        bool answer2 = false;
        bool answer3 = false;
        List<Evidence> evidences = interactHandler.manager.CollectedEvidence;
        foreach(Evidence evidence in evidences)
        {
            if(evidence.EvidenceType() == Type.First)
            {
                answer1 = true;
            }
            else if (evidence.EvidenceType() == Type.Second)
            {
                answer2 = true;
            }
            else if (evidence.EvidenceType() == Type.Third)
            {
                answer3 = true;
            }
        }

        suspects = new List<string>()
        {
            "Jeffery",
            "Patricia",
            "Eric",
        };

        if((answer1&&answer2) || (answer1&&answer3) || (answer2 && answer3))
        {
            suspects.Add("Anna");
        }
    }

    void SetupAccusation()
    {
        int buttonSpacing = 124;
        int index = 0;

        foreach(string s in suspects)
        {
            GameObject optionButton = CreateButton(index, buttonSpacing, s);

            Button button = optionButton.GetComponent<Button>();
            button.onClick.AddListener(delegate { ChangeScene(s); });
            index++;
        }
    }

    private void ChangeScene(string suspect)
    {
        interactHandler.manager.SaveGame();
        SceneManager.LoadScene(suspect + "Ending");
    }

    private GameObject CreateButton(int index, int buttonSpacing, string scene)
    {
        GameObject optionButton = Instantiate(buttonPrefab);
        optionButton.transform.SetParent(parentPanel);
        optionButton.transform.localPosition = new Vector3(0, index * buttonSpacing, 0);
        optionButton.name = "Option(" + index + ")";

        TextMeshProUGUI answer = optionButton.transform.Find("Answer").gameObject.GetComponent<TextMeshProUGUI>();
        answer.text = scene;

        return optionButton;
    }
}

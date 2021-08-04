using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Intermission : DialogueManager
{

    private List<Room> availableRooms;

    protected override void EndDialogue()
    {
        dialogueBox.SetActive(false);
        SetupScenes();
        SetupOptions();
    }

    private void SetupScenes()
    {
        availableRooms = new List<Room>();

        List<Evidence> evidences = interactHandler.manager.CollectedEvidence;
        foreach(Evidence evidence in evidences)
        {
            if (evidence.name.Equals("Explore"))
            {
                availableRooms.Add(Room.DiningHall);
                availableRooms.Add(Room.Kitchen);
                availableRooms.Add(Room.Garden);
            }
            else if (evidence.Name().Equals("UnlockAttic"))
            {
                availableRooms.Add(Room.Attic);
            }
            else if (evidence.Name().Equals("Unlock Bedroom"))
            {
                availableRooms.Add(Room.MasterBedroom);
            }
            else if (evidence.Name().Equals("Hastily Written Note") || evidence.Name().Equals("Personal Note") && !availableRooms.Contains(Room.Library))
            {
                availableRooms.Add(Room.Library);
            }
            else if(evidence.Name().Equals("Unlock Study"))
            {
                availableRooms.Add(Room.Study);
            }
        }
    }

    private void SetupOptions()
    {
        int buttonSpacing = 90;
        int index = -2;


        foreach (Room room in availableRooms)
        {
            string scene = RoomUtils.GetDescription(room);
            if (scene.Equals("")) continue;


            GameObject optionButton = CreateButton(index, buttonSpacing, scene);

            Button button = optionButton.GetComponent<Button>();
            button.onClick.AddListener(delegate { ChangeScene(room); });
            index++;
        }
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

    private void ChangeScene(Room nextRoom)
    {
        interactHandler.manager.VisitRoom(nextRoom);
        interactHandler.manager.SaveGame();
        SceneManager.LoadScene(nextRoom.ToString()+"Act3");
    }


}

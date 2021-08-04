using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;

public class DoorInteractable : IInteractable
{
    //public string description;
    public GameObject buttonPrefab;
    public RectTransform parentPanel;


    private List<string> scenes;
    private List<Room> rooms;
    public Room currentRoom;

    //For unlocking rooms based on evidence
    public List<LinkRoomEvidence> linkRoomEvidences;

    List<GameObject> buttons;

    void Start()
    {
        buttons = new List<GameObject>();
    }

    public override void Interact()
    {
        handler.DisableInteractables();
        SetupScenes();
        SetupOptions();
    }

    void SetupScenes()
    {
        scenes = new List<string>();
        rooms = new List<Room>();

        List<Evidence> collectedEvidences = handler.manager.CollectedEvidence;
        foreach(Evidence evidence in collectedEvidences)
        {
            if (evidence.name.Equals("Explore"))
            {
                rooms.Add(Room.DiningHall);
                rooms.Add(Room.Kitchen);
                rooms.Add(Room.Garden);
            }else if (evidence.Name().Equals("UnlockAttic"))
            {
                Debug.Log("Attic");
                rooms.Add(Room.Attic);
            }else if (evidence.Name().Equals("Unlock Bedroom"))
            {
                rooms.Add(Room.MasterBedroom);
            }else if (evidence.Name().Equals("Hastily Written Note") || evidence.Name().Equals("Personal Note"))
            {
                rooms.Add(Room.Library);
            }else if (evidence.Name().Equals("Unlock Study"))
            {
                rooms.Add(Room.Study);
            }else if (evidence.Name().Equals("Riddle"))
            {
                rooms.Add(Room.WineCellar);
            }
        }
    }

    void SetupOptions(){

        int buttonSpacing = 90;
        int index = -2;


        index = CreateCancelButton(index, buttonSpacing);

        foreach (Room room in rooms)
        {
            string scene = room.ToString();
            string roomName = RoomUtils.GetDescription(room);
            if (scene.Equals("")) continue;

            if (scene.Equals(currentRoom.ToString())) continue;

            GameObject optionButton = CreateButton(index, buttonSpacing, roomName);

            Button button = optionButton.GetComponent<Button>();
            button.onClick.AddListener(delegate { ChangeScene(scene, room); });
            index++;
        }
    }

    int CreateCancelButton(int index, int buttonSpacing)
    {
        GameObject cancelButton = CreateButton(index, buttonSpacing,"Cancel");
        Button button = cancelButton.GetComponent<Button>();
        button.onClick.AddListener(delegate { ChangeScene("Cancel", Room.DiningHall); });

        return index+1;
    }

    GameObject CreateButton(int index, int buttonSpacing, string scene)
    {
        GameObject optionButton = Instantiate(buttonPrefab);
        optionButton.transform.SetParent(parentPanel);
        optionButton.transform.localPosition = new Vector3(0, index * buttonSpacing, 0);
        optionButton.name = "Option(" + index+")";
        buttons.Add(optionButton);

        TextMeshProUGUI answer = optionButton.transform.Find("Answer").gameObject.GetComponent<TextMeshProUGUI>();
        answer.text = scene;

        return optionButton;
    }

    void ChangeScene(string scene, Room nextRoom)
    {
        //Debug.Log(scene);
        if (scene.Equals("Cancel"))
        {
            HideAllButtons();
            handler.EnableInteractables();
        }
        else if (handler.manager.TimePassed == 15.45m)
        {
            handler.manager.NextAct();
            handler.manager.SaveGame();
            SceneManager.LoadScene("Act2Intermission");
        }else if (handler.manager.TimePassed == 16.45m && handler.manager.CurrentAct.Equals(Act.Third))
        {
            handler.manager.NextAct();
            handler.manager.SaveGame();
            handler.manager.AdvanceTime();
            SceneManager.LoadScene("Act3Intermission");
        }
        else
        {
            handler.manager.VisitRoom(nextRoom);
            handler.manager.SaveGame();
            if(handler.manager.CurrentAct.Equals(Act.Third))
            {
                SceneManager.LoadScene(scene + "Act3");
            }
            else
            {
                SceneManager.LoadScene(scene);
            }
        }
    }

    private void HideAllButtons()
    {
        foreach (GameObject button in buttons)
        {
            Destroy(button);
        }
    }

}

[System.Serializable]
public class LinkRoomEvidence
{
    public Evidence evidence;
    public Room room;
}

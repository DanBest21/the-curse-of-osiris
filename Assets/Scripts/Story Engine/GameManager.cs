using System.IO;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public List<Evidence> CollectedEvidence { get; private set; } = new List<Evidence>();
    public List<Character> Characters { get; private set; } = new List<Character>();
    public List<Room> RoomsVisited { get; private set; } = new List<Room>();
    public decimal TimePassed { get; private set; } = 15.15m;
    public Act CurrentAct { get; private set; } = Act.Prologue;
    public int NumberOfKeypadAttempts { get; private set; } = 0;

    private const decimal AdvancementTime = 0.15m;
    private const int KeypadAttemptLimit = 3;
    public const string PuzzleCode = "5133";
    private AudioSource audioSource;

    public static GameManager Instance { get; private set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Multiple Game Managers detected. Only one should exist per scene.");
        }
    }

    public void Start()
    {
        Characters = Resources.LoadAll<CharacterInfo>("Characters").Select(c => new Character(c)).ToList();

        if (File.Exists(StoryEngine.SavePath()))
        {
            LoadGame();
        }

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.volume = 0.5f;
    }

    public void SaveGame()
    {
        StoryEngine.SaveGame(Instance);
    }

    public void DeleteSave()
    {
        StoryEngine.DeleteSave();
    }

    public void LoadGame()
    {
        GameState loadedState = StoryEngine.LoadGame();

        Dictionary<string, Evidence> allEvidence = new Dictionary<string, Evidence>();

        foreach (Evidence evidence in Resources.LoadAll<Evidence>("Evidence"))
        {
            allEvidence.Add(evidence.Name(), evidence);
        }

        CollectedEvidence = loadedState.CollectedEvidence.Select(e => allEvidence[e]).ToList();

        Dictionary<string, CharacterInfo> allCharacters = new Dictionary<string, CharacterInfo>();

        foreach (CharacterInfo character in Resources.LoadAll<CharacterInfo>("Characters"))
        {
            allCharacters.Add(character.Name(), character);
        }

        Characters = loadedState.Characters.Select(c => new Character(allCharacters[c.Name], c.Opinion, c.Location)).ToList();

        RoomsVisited = loadedState.RoomsVisited;

        TimePassed = loadedState.TimePassed;

        CurrentAct = loadedState.CurrentAct;

        NumberOfKeypadAttempts = loadedState.NumberOfKeypadAttempts;
    }

    public void CollectEvidence(Evidence evidence)
    {
        if (!CollectedEvidence.Contains(evidence))
        {
            CollectedEvidence.Add(evidence);
            Debug.Log("Added " + evidence.Name());
        }
    }

    public void VisitRoom(Room room)
    {
        RoomsVisited.Add(room);

        decimal decimalPoint = TimePassed + AdvancementTime - System.Math.Floor(TimePassed + AdvancementTime);

        //Debug.Log("Decimal Point: " + decimalPoint);
        //Debug.Log("Time Passed: " + TimePassed);

        if (decimalPoint >= 0.60m)
        {
            TimePassed = TimePassed - decimalPoint + AdvancementTime + 1;
        }
        else
        {
            TimePassed += AdvancementTime;
        }
    }

    public void AdvanceTime()
    {
        decimal decimalPoint = TimePassed + AdvancementTime - System.Math.Floor(TimePassed + AdvancementTime);

        if (decimalPoint >= 0.60m)
        {
            TimePassed = TimePassed - decimalPoint + AdvancementTime + 1;
        }
        else
        {
            TimePassed += AdvancementTime;
        }
    }

    public void NextAct()
    {
        if (CurrentAct != Act.Epilogue)
        {
            CurrentAct += 1;
        }
        else
        {
            Debug.Log("Illegal call to NextAct() when we are already in the Epilogue.");
        }
    }

    public Character GetCharacter(string name)
    {
        foreach (Character character in Characters)
        {
            if (character.CharacterInfo().Name() == name)
            {
                return character;
            }
        }

        Debug.LogError("Character '" + name + "' was not found in the GameManager.");
        return null;
    }

    public void KeypadFailed()
    {
        NumberOfKeypadAttempts++;
    }

    public bool KeypadLocked()
    {
        return NumberOfKeypadAttempts >= KeypadAttemptLimit;
    }

    public void PlaySFX(string soundName, float volume = 1, bool loop = false)
    {
        if (loop)
        {
            audioSource.clip = Resources.Load<AudioClip>("Audio/SFX/" + soundName);
            audioSource.loop = true;
            audioSource.Play();
        }
        else
        {
            audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/SFX/" + soundName), volume);
        }
    }

    public void StopSFX()
    {
        audioSource.loop = false;
        audioSource.Stop();
    }
}

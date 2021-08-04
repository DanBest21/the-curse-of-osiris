using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class GameState
{
    public List<string> CollectedEvidence { get; private set; }
    public List<CharacterData> Characters { get; private set; }
    public List<Room> RoomsVisited { get; private set; }
    public decimal TimePassed { get; private set; }
    public Act CurrentAct { get; private set; }
    public int NumberOfKeypadAttempts { get; private set; }

    public GameState(GameManager gameManager)
    {
        this.CollectedEvidence = gameManager.CollectedEvidence.Select(e => e.Name()).ToList();
        this.Characters = gameManager.Characters.Select(c => new CharacterData(c)).ToList();
        this.RoomsVisited = gameManager.RoomsVisited;
        this.TimePassed = gameManager.TimePassed;
        this.CurrentAct = gameManager.CurrentAct;
        this.NumberOfKeypadAttempts = gameManager.NumberOfKeypadAttempts;
    }
}

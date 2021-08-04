using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    private CharacterInfo characterInfo;  

    [Range(0, 100)]
    private float opinion = 50;

    private Room location = Room.DiningHall;

    public CharacterInfo CharacterInfo()
    {
        return characterInfo;
    }

    public float Opinion()
    {
        return opinion;
    }

    public Room Location()
    {
        return location;
    }

    public Character(CharacterInfo characterInfo)
    {
        this.characterInfo = characterInfo;
    }

    public Character(CharacterInfo characterInfo, float opinion, Room location)
    {
        this.characterInfo = characterInfo;
        this.opinion = opinion;
        this.location = location;
    }

    public void ChangeLocation(Room location)
    {
        this.location = location;
    }

    public void DecreaseOpinion(float penalty)
    {
        opinion -= penalty;
    }

    public void IncreaseOpinion(float reward)
    {
        opinion += reward;
    }

    public void ChangeOpinion(float opinionChange)
    {
        opinion += opinionChange;
    }

    public bool OpinionCheck(float threshold)
    {
        return opinion >= threshold;
    }

    //public void StoreDialogue(Dialogue dialogue)
    //{
    //    completedDialogue.Add(dialogue);
    //}
}

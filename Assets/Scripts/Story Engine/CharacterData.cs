using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CharacterData
{
    public string Name { get; private set; }
    public float Opinion { get; private set; }
    public Room Location { get; private set; }

    public CharacterData(Character character)
    {
        Name = character.CharacterInfo().Name();
        Opinion = character.Opinion();
        Location = character.Location();
    }
}
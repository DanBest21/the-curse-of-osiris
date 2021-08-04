using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Character")]
public class CharacterInfo : ScriptableObject
{
    [SerializeField]
    private new string name;
    [SerializeField]
    private string nickName;
    [SerializeField]
    private Image image;
    [SerializeField]
    [TextArea(10, 100)]
    private string description;
    [SerializeField]
    private float characterPitch = 1f;
    

    public string Name()
    {
        return name;
    }

    public string NickName()
    {
        return nickName;
    }

    public string Description()
    {
        return description;
    }

    public float CharacterPitch()
    {
        return characterPitch;
    }
}
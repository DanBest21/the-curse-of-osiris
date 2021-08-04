using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Evidence")]
public class Evidence : ScriptableObject
{
    [SerializeField]
    private new string name;
    [SerializeField]
    private Room room;
    [SerializeField]
    [TextArea(3, 30)]
    private string description;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Type type = Type.Useless; 

    public string Name()
    {
        return name;
    }

    public Room Room()
    {
        return room;
    }

    public Type EvidenceType()
    {
        return type;
    }

    public string Description()
    {
        return description;
    }
}

[System.Serializable]
public enum Type
{
    Useless,
    First,
    Second,
    Third
}
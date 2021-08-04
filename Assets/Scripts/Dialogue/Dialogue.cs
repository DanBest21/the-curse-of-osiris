using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="DialogueFile",menuName ="Dialogue")]
public class Dialogue : ScriptableObject
{
    public List<Sentences> sentences;
    public List<OpinionChange> opinionChanges;

    [Header("Setup Choices")]
    public bool isChoice = false;
    [Header("Only works if isChoices is ticked")]
    public Choice[] choices = new Choice[4];

    
    //public bool hasContinuation = false;
    [Header("Allow continuation only if isChoice is not ticked")]
    public Dialogue continuation;

    [Header("A Dialogue can have a piece of evidence linked with it")]
    public Evidence evidence;
}

[System.Serializable]
public class Sentences
{
    public CharacterInfo character;
    [TextArea(3, 30)]
    public string sentence;
}

[System.Serializable]
public class Choice
{
    public string answer;
    public Dialogue answerDialogue;
}

[System.Serializable]
public class OpinionChange
{
    public CharacterInfo character;
    public float opinionChange; 
}

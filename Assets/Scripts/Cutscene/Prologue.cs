using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prologue : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManager manager;
    public List<Sprite> backgroundImages;
    public Image background;

    private bool start;
    private int sentencesCount;
    private int remainingSentences;
    private int previousRemainingSentences;
    private bool shrunk;

    // Start is called before the first frame update
    void Start()
    {
        start = false;
    }

    private void Update()
    {
        if (!start)
        {
            start = true;
            manager.interactHandler.DisableInteractables();
            manager.StartDialogue(dialogue);
            sentencesCount = manager.GetSentenceCount();
            remainingSentences = sentencesCount;
            previousRemainingSentences = remainingSentences;
        }
        else if (previousRemainingSentences != 0 && background != null)
        {
            remainingSentences = manager.GetSentenceCount();
            
            if (remainingSentences < previousRemainingSentences)
            {
                previousRemainingSentences = remainingSentences;
                background.sprite = backgroundImages[sentencesCount - remainingSentences];

                if (background.sprite.name == "Ruby Staff" && !shrunk)
                {
                    background.rectTransform.localScale = new Vector3(0.35f, 0.35f, 0.35f);
                    background.rectTransform.position = new Vector3(background.rectTransform.position.x, background.rectTransform.position.y + 200f);
                    shrunk = true;
                }
                else if (background.sprite.name != "Ruby Staff" && shrunk)
                {
                    shrunk = false;
                    background.rectTransform.localScale = new Vector3(1f, 1f, 1f);
                    background.rectTransform.position = new Vector3(background.rectTransform.position.x, background.rectTransform.position.y - 200f);
                }
            }
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    private Queue<Sentences> sentences;
    private Stack<Dialogue> stack;

    public InteractableHandler interactHandler;
    public GameObject dialogueBox;
    public GameObject buttonPrefab;
    public RectTransform parentPanel;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI highlightText;

    private Dialogue dialogueObj;

    private AudioSource audioSource;

    private bool displaySentence;
    private string currSen;


    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<Sentences>();
        stack = new Stack<Dialogue>();
        dialogueBox.SetActive(false);
        displaySentence = false;

        audioSource = gameObject.AddComponent<AudioSource>();

        audioSource.volume = 0.1f;
        audioSource.clip = Resources.Load<AudioClip>("Audio/SFX/Dialogue");
    }

    public virtual void StartDialogue(Dialogue dialogue)
    {
        dialogueBox.SetActive(true);
        dialogueBox.transform.Find("ContinueButton").gameObject.SetActive(true);
        sentences.Clear();

        dialogueObj = dialogue;

        if(dialogue.evidence != null)
        {
            DialogueEvidence();
        }

        foreach(Sentences sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        highlightText.text = OpinionChanges();
        Invoke("DisableNotification", 2);

        DisplayNextSentence();
    }

    public string OpinionChanges()
    {
        string sen = "";
        for (int i = 0; i < dialogueObj.opinionChanges.Count; i++)
        {
            if (dialogueObj.opinionChanges[i].character != null )
            {
                string charName = dialogueObj.opinionChanges[i].character.Name();
                string nickName = dialogueObj.opinionChanges[i].character.NickName();
                float change = dialogueObj.opinionChanges[i].opinionChange;
                if (i == 0)
                {
                    sen = nickName;
                }
                else if (i == (dialogueObj.opinionChanges.Count-1))
                {
                    sen = sen + " and "+ nickName;
                }
                else
                {
                    sen = sen + ", " + nickName;
                }
                Character character = interactHandler.manager.GetCharacter(charName);
                character.ChangeOpinion(change);
                Debug.Log(nickName + " opinion: " + character.Opinion());
            }
        }

        if (!sen.Equals(""))
        {
            sen = sen + " will remember that.";
        }

        return sen;
    }

    void DisableNotification()
    {
        highlightText.text = "";
    }

    public void DisplayNextSentence()
    {
        if (!displaySentence)
        {
            if (sentences.Count == 0)
            {
                if (dialogueObj.continuation != null && !dialogueObj.isChoice)
                {
                    //Debug.Log("Cont");
                    StartDialogue(dialogueObj.continuation);
                    return;
                }

                if (dialogueObj.isChoice)
                {
                    dialogueBox.transform.Find("ContinueButton").gameObject.SetActive(false);

                    SetupOptions();
                    return;

                }

                EndDialogue();
                return;

            }
        }
        


        
        if (displaySentence)
        {
            audioSource.loop = false;
            audioSource.volume = 0;
            
            StopAllCoroutines();
            dialogueText.text = currSen;
            displaySentence = false;

        }
        else
        {
            Sentences sen = sentences.Dequeue();
            currSen = sen.sentence;
            if (sen.character != null)
            {
                nameText.text = sen.character.NickName();
                audioSource.pitch = sen.character.CharacterPitch();
            }
            else
            {
                nameText.text = "";
                audioSource.pitch = 1f;
            }
            StartCoroutine(TypeSentence(sen.sentence));
        }
        
        
    }

    IEnumerator TypeSentence (string sentence)
    {
        displaySentence = true;
        dialogueText.text = "";

        audioSource.loop = true;
        audioSource.volume = 0.1f;
        audioSource.Play();

        foreach (char letter in sentence.ToCharArray())
        {   
            dialogueText.text += letter;
            yield return new WaitForSeconds(0.01f);
        }

        audioSource.loop = false;
        audioSource.volume = 0;

        displaySentence = false;
    }

    protected virtual void EndDialogue()
    {
        dialogueBox.SetActive(false);
        interactHandler.EnableInteractables();
    }

    void SetupOptions()
    {

        int buttonSpacing = 124;
        int index = 0;
        List<GameObject> buttons = new List<GameObject>();

        foreach(Choice choice in dialogueObj.choices){

            if(choice.answer.Equals("") || choice.answerDialogue == null){
                continue;
            } 

            index++;
            if(index > 4) break;

            GameObject choiceButton = Instantiate(buttonPrefab);
            choiceButton.transform.SetParent(parentPanel);
            choiceButton.transform.localPosition = new Vector3(0,(index-1)*buttonSpacing,0);
            choiceButton.name = "Choice"+index;

            TextMeshProUGUI answer = choiceButton.transform.Find("Answer").gameObject.GetComponent<TextMeshProUGUI>();
            answer.text = choice.answer;

            DialogueTrigger dt = choiceButton.AddComponent<DialogueTrigger>();
            dt.dialogue = choice.answerDialogue;


            Button button = choiceButton.GetComponent<Button>();
            button.onClick.AddListener(dt.TriggerDialogue);
            buttons.Add(choiceButton);

            
        }

        foreach(GameObject button in buttons){
            button.GetComponent<Button>().onClick.AddListener(() => HideAllButtons(buttons));
        }
    }

    void HideAllButtons(List<GameObject> buttons){
        
        foreach(GameObject button in buttons){
            Destroy(button);
        }

    } 

    void DialogueEvidence()
    {
        //Do something with dialogue's evidence
        interactHandler.manager.CollectEvidence(dialogueObj.evidence);
        //Debug.Log("Added evidence "+dialogueObj.evidence.Name());
    }

    public int GetSentenceCount()
    {
        return sentences.Count;
    }
}

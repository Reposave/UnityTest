using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    Pedestrian pedest;

    public Animator animator;

    private Queue<string> sentences; //FIFO

    // For some reason, this isn't run when the DialogueManager is created.
    void Start()
    {
        /*sentences = new Queue<string>(); //For some reason, this has a delayed start which will empty the queue after some seconds which is what caused the
        dialogue ending after one word.*/
    }

    // Update is called once per frame
    public void StartDialogue(Dialogue dialogue, Pedestrian ped) {
        sentences = new Queue<string>();
        pedest = ped;

        Debug.Log("Starting conversation with " + ped.my_name);

        //animator.SetBool("isOpen", true);

        nameText.text = ped.my_name;

        sentences.Clear(); //removing sentences from previous conversations.

        foreach (string sentence in dialogue.sentences) {
            sentences.Enqueue(sentence); //adding all sentences for the dialogue into the queue.
            //Debug.Log(sentence);
        }

        //Debug.Log(sentences.Count);
        //Debug.Log("After Enqueue");

        DisplayNextSentence();
    }

    public void DisplayNextSentence() {
        if (sentences.Count == 0) { //If there are no sentences left, end the dialogue.
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue(); //get the next sentence.
        //dialogueText.text = sentence;

        //Debug.Log("Before Stop");
        //Debug.Log(sentence);
        //Debug.Log(sentences.Count);
        StopAllCoroutines(); //If TypeSentence is already running, stop doing so and run a new coroutine.

        //Debug.Log(sentences.Count);

        StartCoroutine(TypeSentence(sentence));

        //Debug.Log("AfterStart");
        //Debug.Log(sentences.Count);
        //Debug.Log(sentence);

    }

    IEnumerator TypeSentence(string sentence) { //To display each character one by one in an animated fashion.
        dialogueText.text = "";
        //How would you set a timer in between characters? e.g sentence 5
        foreach (char letter in sentence.ToCharArray()) { //Turned sentence into string array.

            dialogueText.text += letter;

            yield return null;  //wait one frame.
        }
        //Debug.Log("Ienumerator");
        //Debug.Log(sentence);
        //Debug.Log(sentences.Count);

        yield return new WaitForSeconds(1);
        DisplayNextSentence();
    }
    public void InterruptMe(){ //Used to interrupt low priority dialogue.
        StopAllCoroutines();
        EndDialogue();
    }

    void EndDialogue() {
        Debug.Log("End of conversation");
        animator.SetBool("isOpen", false);
        pedest.SpawnedText = false; //Allow spawning of another Manager.
        Destroy(gameObject);
    }
}

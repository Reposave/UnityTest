using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pedestrian : MonoBehaviour
{
    public GameObject spawnDialogue;
    public GameObject dialogueManager;

    public GameObject canva;
    public Camera cam;    // Camera containing the canvas
    // Start is called before the first frame update
    public bool SpawnedText = false;

    GameObject myDialogue;
    GameObject myDialogueManager;

    public Rigidbody2D rb;
    private float linearDrag = 6f;
    private float mass = 0.01f;

    public string my_name;

    public Dialogue person_trigger_dialogue;
    public Dialogue person_collide_dialogue;

    public Dialogue vehicle_trigger_dialogue;
    public Dialogue vehicle_collide_dialogue;
    
    private bool collision_happened = false;
    

    void Start()
    {
       canva = GameObject.Find("Canvas");
       cam = GameObject.Find("Main Camera").GetComponent<Camera>();

       rb = GetComponent<Rigidbody2D>();
        rb.mass = mass;
        rb.drag = linearDrag;
        rb.freezeRotation = true;

        
    }

    // Update is called once per frame
    void Update()
    {
        /*if(SpawnedText){
            Vector3 screenPos = cam.WorldToScreenPoint(this.transform.position);
            float h = Screen.height;
            float w = Screen.width;
            float x = screenPos.x - (w / 2);
            float y = screenPos.y - (h / 2);
            float s = canva.GetComponent<Canvas>().scaleFactor;

            myDialogue.transform.positio(new Vector2(x, y)/s + new Vector2(120,160));
            //myDialogue.transform.SetParent(canva.transform,false);
        }*/
    }
    private void CreateDialogue(Dialogue dial){
        if(!SpawnedText){
            SpawnedText = true;
            Vector3 screenPos = cam.WorldToScreenPoint(this.transform.position);
            float h = Screen.height;
            float w = Screen.width;
            float x = screenPos.x - (w / 2);
            float y = screenPos.y - (h / 2);
            float s = canva.GetComponent<Canvas>().scaleFactor;

            myDialogueManager = Instantiate(dialogueManager, new Vector2(0, 0), Quaternion.identity);
            myDialogue = Instantiate(spawnDialogue, new Vector2(x, y)/s + new Vector2(120,160), Quaternion.identity);

            foreach (Transform child in myDialogue.transform){
                if (child.name == "Name"){
                    myDialogueManager.GetComponent<DialogueManager>().nameText = child.GetComponent<Text>();
                }else if(child.name == "Dialogue"){
                    myDialogueManager.GetComponent<DialogueManager>().dialogueText = child.GetComponent<Text>();
                }
            }
            myDialogueManager.GetComponent<DialogueManager>().animator = myDialogue.GetComponent<Animator>();
            myDialogueManager.GetComponent<DialogueManager>().StartDialogue(dial,this);

            myDialogue.transform.SetParent(canva.transform,false);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.tag == "Player"){
            StartCoroutine(Counter(person_trigger_dialogue));
        }
        else if(other.tag == "Vehicle"){
            StartCoroutine(Counter(vehicle_trigger_dialogue));
        }
    }
    private void OnCollisionEnter2D(Collision2D other) {

        InterruptDialogue();

        if(other.gameObject.tag == "Player"){
            collision_happened = true;
            CreateDialogue(person_collide_dialogue);
        }
        else if(other.gameObject.tag == "Vehicle"){
            collision_happened = true;
            CreateDialogue(vehicle_collide_dialogue);
        }
        collision_happened = false;
    }
    
    void InterruptDialogue(){
        if(SpawnedText == true){
            myDialogueManager.GetComponent<DialogueManager>().InterruptMe();
            SpawnedText = false;
        }
    }

    IEnumerator Counter(Dialogue dial) { //Waits to see if we triggered only or the player will eventually collide with the pedestrian.
        yield return new WaitForSeconds(0.4f);
        if(!collision_happened){
            CreateDialogue(dial);
        }
    }

}

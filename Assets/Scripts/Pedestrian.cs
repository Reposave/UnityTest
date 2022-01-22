using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pedestrian : MonoBehaviour
{
    public GameObject spawnDialogue;
    public GameObject canva;
    public Camera cam;    // Camera containing the canvas
    // Start is called before the first frame update
    private bool SpawnedText = false;
    GameObject myDialogue;

    public Rigidbody2D rb;
    private float linearDrag = 6f;
    private float mass = 0.01f;

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

    private void OnTriggerEnter2D(Collider2D other) {
        if(!SpawnedText){
            Vector3 screenPos = cam.WorldToScreenPoint(this.transform.position);
            float h = Screen.height;
            float w = Screen.width;
            float x = screenPos.x - (w / 2);
            float y = screenPos.y - (h / 2);
            float s = canva.GetComponent<Canvas>().scaleFactor;

            myDialogue = Instantiate(spawnDialogue, new Vector2(x, y)/s + new Vector2(120,160), Quaternion.identity);
            myDialogue.transform.SetParent(canva.transform,false);
            SpawnedText = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        
    }
}

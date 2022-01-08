using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float Maxspeed = 10.0f;
    float CurrentSpeed=0f;

    bool inVehicle=false;
    bool nearVehicle=false;

    Vehicle Vehiclez;

    public Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(!inVehicle){
            ButtonPressed();
        }else{
            if(Input.GetKeyDown(KeyCode.E)){
            //Exit Vehicle...
                Vehiclez.PlayerOcc = false;
                inVehicle = false;
                transform.position = Vehiclez.CurrentPos() + new Vector3(1,0,0);
                InCar(false);
            }
        }
    }
    void Move(Vector2 direction){

        CurrentSpeed= Maxspeed;

        rb.AddForce((Vector3)direction*CurrentSpeed*Time.deltaTime);
        //transform.localPosition+=(Vector3)direction*CurrentSpeed*Time.deltaTime;
    }
    void ButtonPressed(){
        if(Input.GetKey(KeyCode.LeftArrow)){
            Move(Vector2.left);
        }

        else if (Input.GetKey(KeyCode.RightArrow)){
            Move(Vector2.right);
        }

        else if (Input.GetKey(KeyCode.DownArrow)){
            Move(Vector2.down);
        }
        else if(Input.GetKey(KeyCode.UpArrow)){
            Move(Vector2.up);
        }
        else if(Input.GetKeyDown(KeyCode.E)){
            //Enter Vehicle...
            if(nearVehicle){
                Vehiclez.PlayerOcc = true;
                inVehicle = true;
                InCar(true);
            }
        }
    }
    private void InCar(bool val){
        val = !val; //For readability's sake.
        this.GetComponent<SpriteRenderer>().enabled = val;
        this.GetComponent<CircleCollider2D>().enabled = val;
    }
    private void OnTriggerEnter2D(Collider2D other) {
        //If you exit a Car when another is next to you, the other car may not be detected thus you can't enter it.
        if(other.tag=="Vehicle"){
            nearVehicle=true;
            Vehiclez = other.GetComponent<Vehicle>();
            Debug.Log("Car Detected!");
        }
    }
    private void OnTriggerExit2D(Collider2D other) {
        if(other.tag=="Vehicle"){
            nearVehicle=false;
            if(!inVehicle){
                Vehiclez = null;
                Debug.Log("Car left.");
            }
            Debug.Log("Car left player behind.");
        }    
    }
}

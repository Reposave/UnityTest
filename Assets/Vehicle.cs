using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{   
    public float Maxspeed = 60.0f;
    float CurrentSpeed=0f;

    public bool PlayerOcc = false; //Tracks whether player has occupied this vehicle.
    public Rigidbody2D rb;
    public float linearDrag = 2f;
    public float mass = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.mass = mass;
        rb.drag = linearDrag;
        rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerOcc){
            ButtonPressed();
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
    }
    public Vector3 CurrentPos(){
        return transform.position;
    }
}

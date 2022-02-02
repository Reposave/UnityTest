using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vehicle : MonoBehaviour
{   
    public float Maxspeed = 6000.0f;
    float CurrentSpeed=0f;

    public bool PlayerOcc = false; //Tracks whether player has occupied this vehicle.
    public Rigidbody2D rb;
    public Rigidbody2D wheel;
    public GameObject arrow;
    public float linearDrag = 10f;
    public float mass = 0.1f;

    //Quick Dirty Test...
    public float reversing = 0; //Reversing can have two values, 0 or 180 only! This is used to flip the force directions.

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in transform){
                if (child.name == "Engine"){
                    //child.GetComponent<SpriteRenderer>().enabled = false;
                    rb = child.GetComponent<Rigidbody2D>();
                }else if (child.name == "Wheels"){
                    wheel = child.GetComponent<Rigidbody2D>();
                }
            }
        //rb = GetComponent<Rigidbody2D>();
        rb.mass = mass;
        rb.drag = linearDrag;
        //rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(PlayerOcc){
            ButtonPressed();
        }
        arrow.transform.eulerAngles = gameObject.transform.eulerAngles;
    }
    void Move(Vector2 direction,Rigidbody2D rbx,float Cspeed){

        rbx.AddForce((Vector3)direction*Cspeed*Time.deltaTime);
        //transform.localPosition+=(Vector3)direction*CurrentSpeed*Time.deltaTime;
    }
    void ButtonPressed(){

        if (Input.GetKey(KeyCode.DownArrow)){
            Vector2 a = DegreeToVector2(gameObject.transform.eulerAngles.z-90);
            reversing = 180;
            Move(a,rb, Maxspeed);
            ButtonPressedLR();
        }
        else if(Input.GetKey(KeyCode.UpArrow)){
            Vector2 a = DegreeToVector2(gameObject.transform.eulerAngles.z+90);
            reversing = 0;
            Move(a,rb, Maxspeed);
            ButtonPressedLR(); 
        }

        ButtonPressedLR();
    }

    void ButtonPressedLR(){
        //This function allows for simultaneous button presses.
        if(Input.GetKey(KeyCode.LeftArrow)){
            Vector2 a = DegreeToVector2(gameObject.transform.eulerAngles.z + 180 + reversing);
            Move(a,wheel,rb.velocity.magnitude*10);
        }

        else if (Input.GetKey(KeyCode.RightArrow)){
            Vector2 a = DegreeToVector2(gameObject.transform.eulerAngles.z + reversing);
            Move(a,wheel,rb.velocity.magnitude*10);
        }

    }

    public Vector3 CurrentPos(){
        return transform.position;
    }

    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }
      
    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Quest : MonoBehaviour
{
    public GameObject[] chkpoints= new GameObject[5];
    int chkindex = 0;

    public Text tex; //Can this be Prefabed?
    // Start is called before the first frame update
    void Start()
    {
        tex = GameObject.Find("MissionText").GetComponent<Text>();
        Activate();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Activate(){
        //Find All the Children GameObjects and add them to the Array.
        /*int children = transform.childCount;
         for (int i = 0; i < children; ++i){
             print("For loop: " + transform.GetChild(i));
             chkpoints[i] = transform.GetChild(i).GetComponent<GameObject>();
         }*/

         foreach(GameObject chk in chkpoints){
            if(chk!=null){
                chk.SetActive(false);
            }
        }
        chkindex = 0;
        chkpoints[chkindex].SetActive(true); //Element 0 is usually the first Checkpoint.
    }

    public void LoadNextCheckPoint(){
        
        chkpoints[chkindex].SetActive(false);
        ++chkindex;
        if(chkpoints[chkindex]!=null){ //What happens if it checks an element outside the array length??
            tex.text = "Deliver the package";
            chkpoints[chkindex].SetActive(true); 
        }else{
            tex.text = "Well Done!";
            QuestOver();
        }
    }
    private void QuestOver(){
        Destroy(this.gameObject);
    }

}

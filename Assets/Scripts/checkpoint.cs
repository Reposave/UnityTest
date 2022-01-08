using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    public enum CheckType {Start,Checkpoint, MultiCheck, End} //This may not matter yet.
    public CheckType checkpoint_type;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other) {
        //if(other.GetComponent<Player>()){ //Rather use Layers
            transform.parent.GetComponent<Quest>().LoadNextCheckPoint();
        //}    
    }
}

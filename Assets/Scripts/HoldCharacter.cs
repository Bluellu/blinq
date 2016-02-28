using UnityEngine;
using System.Collections;

public class HoldCharacter : MonoBehaviour {


	void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.tag == "Player")
        {            
            col.transform.parent = gameObject.transform.parent;
        }
        
    }

    void OnTriggerExit(Collider col)
    {
        
        if (col.gameObject.tag == "Player")
        {            
            col.transform.parent = null;
        }
    }
}

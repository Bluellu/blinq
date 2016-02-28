using UnityEngine;
using System.Collections;

public class HoldCharacter : MonoBehaviour {


	void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("entered Trigger");
            col.transform.parent = gameObject.transform.parent;
        }
        
    }

    void OnTriggerExit(Collider col)
    {
        
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("exitered Trigger");
            col.transform.parent = null;
        }
    }
}

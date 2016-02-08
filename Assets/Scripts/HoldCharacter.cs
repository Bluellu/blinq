using UnityEngine;
using System.Collections;

public class HoldCharacter : MonoBehaviour {

	void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            col.transform.parent = gameObject.transform;
        }
        
    }

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            col.transform.parent = null;
        }
    }
}

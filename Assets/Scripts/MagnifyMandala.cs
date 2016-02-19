using UnityEngine;
using System.Collections;

public class MagnifyMandala : MonoBehaviour {
	public TeleportationController tc;
	void OnTriggerEnter (Collider col) {
        if (col.gameObject.name == "Player")
        {
            //tc.increaseRadius();
            //tc.onMag = true;
        }
	}
    void OnTriggerExit (Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            //tc.transform.position = tc.getPlayerOrigin();
           // tc.active = false;
           // tc.onMag = false;
        }
    }
}

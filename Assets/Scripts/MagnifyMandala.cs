using UnityEngine;
using System.Collections;

public class MagnifyMandala : MonoBehaviour {
	void OnTriggerEnter (Collider col)
	{
		TeleportationController tc = col.transform.Find ("TeleTarget").GetComponent<TeleportationController> ();
		Vector3 origin = tc.getPlayerOrigin();
		Vector3 distance = tc.transform.position - origin;
		Vector3 new_distance = new Vector3 (distance.x * 3, distance.y, distance.z * 3);
		tc.setRadius (new_distance);
	}

	void OnTriggerExit(Collider col)
	{
		TeleportationController tc = col.transform.Find ("TeleTarget").GetComponent<TeleportationController> ();
		Vector3 origin = tc.getPlayerOrigin();
		Vector3 distance = tc.transform.position - origin;
		Vector3 new_distance = new Vector3 (distance.x / 3, distance.y, distance.z / 3);
		tc.setRadius (new_distance);
	}
}

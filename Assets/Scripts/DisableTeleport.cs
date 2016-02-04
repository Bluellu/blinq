using UnityEngine;
using System.Collections;

public class DisableTeleport : MonoBehaviour {
	public bool charHere;
	void OnTriggerEnter (Collider col)
	{
		col.transform.Find ("TeleTarget").GetComponent<TeleportationController>().canTeleport = false;


	}

	void OnTriggerExit(Collider col)
	{
		col.transform.Find ("TeleTarget").GetComponent<TeleportationController> ().canTeleport = true;
	}
}

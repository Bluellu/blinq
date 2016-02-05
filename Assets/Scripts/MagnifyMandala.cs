using UnityEngine;
using System.Collections;

public class MagnifyMandala : MonoBehaviour {
	public TeleportationController tc;
	void OnTriggerStay (Collider col) {
		tc.increaseRadius ();
	}
}

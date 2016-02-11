using UnityEngine;
using System.Collections;

public class DisableTeleport : MonoBehaviour {
    public TeleportationController tc;
    void OnTriggerEnter (Collider col)
	{
        if (col.gameObject.name == "Player")
        {
            StartCoroutine(disableTeleport());
        }
	}

	void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            tc.canTeleport = true;
        }
	}

    IEnumerator disableTeleport()
    {
        Debug.Log("Inside disableTeleport()");
        yield return new WaitForSeconds(1);
        tc.canTeleport = false;
    }
}

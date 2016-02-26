using UnityEngine;
using System.Collections;

public class DisableTeleport : MonoBehaviour {
    public TeleportationController tc;
    public Transform mandala;
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
            mandala.GetComponent<Renderer>().enabled = true;
        }
	}

    IEnumerator disableTeleport()
    {
        Debug.Log("Inside disableTeleport()");
        yield return new WaitForSeconds(0.3f);
        tc.canTeleport = false;
        mandala.GetComponent<Renderer>().enabled = false;
    }
}

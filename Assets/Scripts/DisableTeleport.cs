using UnityEngine;
using System.Collections;

public class DisableTeleport : MonoBehaviour {
    private MandalaMovementController MMC;
    private TeleportationController TC;
    private MarkerCollisionController MColC;
    private GameObject MandalaObject, Mandala;

    void Start()
    {
        MandalaObject = GameObject.FindGameObjectWithTag("MandalaMarker");
        Mandala = GameObject.FindGameObjectWithTag("Mandala");
        MMC = MandalaObject.GetComponent<MandalaMovementController>();
        TC = Mandala.GetComponent<TeleportationController>();
        MColC = Mandala.GetComponent<MarkerCollisionController>();
    }
    void OnTriggerEnter (Collider col)
	{
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("EnteredTrigger");
            TC.canActivateTele = false;
            MMC.canTeleport = false;
            MColC.onDisableTile = true;
            Mandala.GetComponent<MeshRenderer>().enabled = false;
        }
	}

	void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("ExitTrigger");
            TC.canActivateTele = true;
            MMC.canTeleport = true;
            MColC.onDisableTile = false;
            Mandala.GetComponent<MeshRenderer>().enabled = true;
        }
	}

}

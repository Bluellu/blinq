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
           // MMC.canTeleport = false;
            MColC.onDisableTile = true;

            foreach (Transform child in Mandala.transform)
            {
                if (child.name == "mandala_gold")
                {
                    child.GetComponent<SpriteRenderer>().enabled = false;
                }
                if (child.name == "mandala_bad")
                {
                    child.GetComponent<SpriteRenderer>().enabled = true;
                }
            }

        }
	}

	void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log("ExitTrigger");
            TC.canActivateTele = true;
            //MMC.canTeleport = true;
            MColC.onDisableTile = false;

            foreach (Transform child in Mandala.transform)
            {
                if (child.name == "mandala_gold")
                {
                    child.GetComponent<SpriteRenderer>().enabled = true;
                }
                if (child.name == "mandala_bad")
                {
                    child.GetComponent<SpriteRenderer>().enabled = false;
                }
            }
        }
	}

}

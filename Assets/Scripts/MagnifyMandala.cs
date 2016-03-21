using UnityEngine;
using System.Collections;

public class MagnifyMandala : MonoBehaviour {

    public float MagnificationAmount;
    private MandalaMovementController MMC;
    //private TeleportationController TC;
    private GameObject MandalaObject;
    void Start()
    {
        MandalaObject = GameObject.FindGameObjectWithTag("MandalaMarker");
        //Mandala = GameObject.FindGameObjectWithTag("Mandala");
        MMC = MandalaObject.GetComponent<MandalaMovementController>();
        //TC = Mandala.GetComponent<TeleportationController>();

    }
	void OnTriggerStay (Collider col) {
        if (col.gameObject.tag == "Player")
        {
            
            MMC.AddMandalaRadius(MagnificationAmount);
        }
	}
    void OnTriggerExit (Collider col)
    {
        if (col.gameObject.tag == "Player")
        {            
            MMC.SubMandalaRadius(MagnificationAmount - MMC.fRadius);
            
        }
    }
}

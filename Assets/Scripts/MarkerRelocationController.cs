using UnityEngine;
using System.Collections;

public class MarkerRelocationController : MonoBehaviour
{

    public GameObject MandalaObject;
	public GameObject MandalaObjectSelf;

    // Use this for initialization
    void Start()
    {
		ParticleSystem ps = MandalaObject.GetComponent<ParticleSystem>();
		var Em = ps.emission;

		Em.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Mandala")
        {
            TeleportationController TC = other.GetComponent<TeleportationController>();
            TC.onRelocationTile = true;
            TC.vRelocationTilePos = MandalaObject.transform.position;

			ParticleSystem ps = MandalaObject.GetComponent<ParticleSystem>();
			var Em = ps.emission;
			Em.enabled = true;

			ParticleSystem ps2 = MandalaObjectSelf.GetComponent<ParticleSystem>();
			var Em2 = ps2.emission;
			Em2.enabled = true;
        }    
    }


    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Mandala")
        {
            TeleportationController TC = other.GetComponent<TeleportationController>();
            TC.onRelocationTile = false;            

			ParticleSystem ps = MandalaObject.GetComponent<ParticleSystem>();
			var Em = ps.emission;
			Em.enabled = false;

			ParticleSystem ps2 = MandalaObjectSelf.GetComponent<ParticleSystem>();
			var Em2 = ps2.emission;
			Em2.enabled = false;
        }

    }
}
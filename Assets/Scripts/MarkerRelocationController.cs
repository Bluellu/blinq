using UnityEngine;
using System.Collections;

public class MarkerRelocationController : MonoBehaviour
{

    public GameObject MandalaObject;
    // Use this for initialization
    void Start()
    {
        MandalaObject.GetComponent<MeshRenderer>().enabled = false;

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Mandala")
        {
            TeleportationController TC = other.GetComponent<TeleportationController>();
            TC.onRelocationTile = true;
            TC.vRelocationTilePos = MandalaObject.transform.position;
            MandalaObject.GetComponent<MeshRenderer>().enabled = true;
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Mandala")
        {
            TeleportationController TC = other.GetComponent<TeleportationController>();
            TC.onRelocationTile = false;            
            MandalaObject.GetComponent<MeshRenderer>().enabled = false;
        }

    }
}

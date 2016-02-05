using UnityEngine;
using System.Collections;

public class MarkerCollisionController : MonoBehaviour {

    private TeleportationController teleController;
    // Use this for initialization
    void Start () {
        teleController = gameObject.GetComponent<TeleportationController>();

    }
	
	// Update is called once per frame
	void Update () {
        
	
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "LevelModel")
        {
            Debug.Log("Collided withait");

            MeshRenderer MRrenderer = other.gameObject.GetComponent<MeshRenderer>();
            Vector3 vDimensions = MRrenderer.bounds.size;
            teleController.fTeleHeight += vDimensions.y;
            teleController.ChangeMandalaHeight(vDimensions.y);
        }


    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "LevelModel")
        {
            MeshRenderer MRrenderer = other.gameObject.GetComponent<MeshRenderer>();
            Vector3 vDimensions = MRrenderer.bounds.size;
            teleController.ChangeMandalaHeight(-vDimensions.y);
            teleController.fTeleHeight -= vDimensions.y;
        }
    }
}

using UnityEngine;
using System.Collections;

public class TileDelayDestroy : MonoBehaviour {
    public float delay;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            
            Destroy(gameObject.transform.parent.gameObject, delay);
        }
    }
}

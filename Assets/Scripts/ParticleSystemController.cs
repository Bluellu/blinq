using UnityEngine;
using System.Collections;

public class ParticleSystemController : MonoBehaviour {

    private ParticleSystem PartPS;

    // Use this for initialization
    void Start () {
        PartPS = gameObject.GetComponent<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        if (!PartPS.IsAlive())
            Destroy(gameObject);
    }
}

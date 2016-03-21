using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour {
    public Transform spawnPoint;
    Transform player;

	// Use this for initialization
	void Start () {
        player = GameObject.FindWithTag("Player").GetComponent<Transform>(); ;

    }
	
	// Update is called once per frame
	void OnTriggerEnter (Collider obj) {
        if (obj.gameObject.tag == "Player") {
            player.transform.position = spawnPoint.position;
        }
	
	}
}

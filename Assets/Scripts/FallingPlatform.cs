using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour {
    private Transform platform;
    
	// Use this for initialization
	void Start () {
        platform = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    // If player touches platform, start falling routine.
    void OnTriggerEnter(Collider obj)  {
        if (obj.gameObject.name == "Player")  {
            Debug.Log("COLLISION");
            StartCoroutine(falling());    
        }
    }

    /* Drop platform after a few seconds */
    IEnumerator falling() { 
        yield return new WaitForSeconds(0.8f);
        platform.position = Vector3.MoveTowards(platform.position, new Vector3(platform.position.x, platform.position.y - 20, platform.position.z), 5 * Time.deltaTime);
        StartCoroutine(destroyPlatform());
    }

    IEnumerator destroyPlatform() {
        yield return new WaitForSeconds(2);
        Destroy(gameObject);
    }
}

using UnityEngine;
using System.Collections;

public class FallingPlatform : MonoBehaviour {
    private Transform platform;

    public float fallingTime;
    public float fallingSpeed;
    public float destroyTime;

    private bool isFalling;

	// Use this for initialization
	void Start () {
        platform = GetComponent<Transform>();
        isFalling = false;
    }

    void Update() {
        if (isFalling) {
            //Move platform downwards continuously.
            transform.Translate(Vector3.down * fallingSpeed * Time.deltaTime);
        }
    }


    // If player touches platform, start falling routine.
    void OnTriggerEnter(Collider obj)  {
        if (obj.gameObject.name == "PlayerAttached")  {
            StartCoroutine(falling());    
        }
    }


    /* Drop platform after the delimited seconds. */
    IEnumerator falling() { 
        yield return new WaitForSeconds(fallingTime);
        isFalling = true;

        StartCoroutine(destroyPlatform());
    }


    /* Destroy platform after the delimited seconds. */
    IEnumerator destroyPlatform() {
        yield return new WaitForSeconds(destroyTime);
        Destroy(gameObject);
    }
}

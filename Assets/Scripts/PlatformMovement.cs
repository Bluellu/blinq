using UnityEngine;
using System.Collections;

public class PlatformMovement : MonoBehaviour {

    private int zMove = 0;
    private bool bUp = true;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate() {
        if (zMove < 100 && bUp)
        {
            transform.Translate(0, 0, 2 * Time.deltaTime);
            zMove += 1;
        }
        if (zMove == 100)
        {
            bUp = false;
            
        }
        if (zMove > -100 && !bUp)
        {
            transform.Translate(0, 0, -2 * Time.deltaTime);
            zMove -= 1;
        }
        if (zMove == -100)
        {
            bUp = true;
        }
    }
}

using UnityEngine;
using System.Collections;

public class TeleportationCircleController : MonoBehaviour {

    public GameObject PlayerChar;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float posX = PlayerChar.transform.position.x ;
        float posY = PlayerChar.transform.position.y + 0.2f;
        float posZ = PlayerChar.transform.position.z ;
        transform.position = new Vector3(posX, posY, posZ);

    }
}

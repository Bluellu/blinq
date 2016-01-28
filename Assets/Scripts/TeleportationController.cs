using UnityEngine;
using System.Collections;

public class TeleportationController : MonoBehaviour {
    public GameObject PlayerObj;
    public GameObject TeleportationObj;
    public float fMovementRange = 10;
    public float fSpeed = 4.5f;
    private float fSpacing = 1.0f;
    private Vector3 vPos;


    // Use this for initialization
    void Start () {
        //vPos = TeleportationObj.transform.position;
        //vPos = new Vector3(0, 0, 0);

    }
	
	// Update is called once per frame
	void Update () {

        Vector3 vDistance = TeleportationObj.transform.position - PlayerObj.transform.position;
        Debug.DrawLine(PlayerObj.transform.position, TeleportationObj.transform.position, new Color(1, 0 ,0));
        Debug.Log(vDistance.sqrMagnitude);
        //if (vDistance.sqrMagnitude < fMovementRange)
        //{
            vPos = new Vector3(0, 0, 0);
            //Move teleportation object 
            if (Input.GetKey("i"))
            {
                vPos.z += fSpacing;
            }
            if (Input.GetKey("j"))
            {
                vPos.x -= fSpacing;
            }
            if (Input.GetKey("k"))
            {
                vPos.z -= fSpacing;
            }
            if (Input.GetKey("l"))
            {
                vPos.x += fSpacing;
            }

            TeleportationObj.transform.Translate(vPos * fSpeed * Time.deltaTime);
        //}

        //Teleport player to object
        if (Input.GetKey("e"))
        {
            Vector3 vTeleLocation = new Vector3(TeleportationObj.transform.position.x, 1.58f, TeleportationObj.transform.position.z);
            PlayerObj.transform.position = vTeleLocation;
        }

    }
}

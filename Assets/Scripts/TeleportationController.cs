using UnityEngine;
using System.Collections;

public class TeleportationController : MonoBehaviour {
    public GameObject PlayerObj;

    public float fMovementRange = 10;
    public float fSpeed = 4.5f;
    public float fRadius =5;

    //State 0, tele marker follows
    public int nState;
    private float fSpacing;
    private Vector3 vPos;
    private Vector3 axis = Vector3.up;
    private Vector3 vPlayerOrigin;
    public float fRotationSpeed = 2.0f;


    // Use this for initialization
    void Start () {
        vPlayerOrigin = PlayerObj.transform.position;

        transform.position = new Vector3(vPlayerOrigin.x + 4, vPlayerOrigin.y, vPlayerOrigin.z);
        

        nState = 3;
        fRadius = 4.0f;

        fSpacing = 1.0f;


    }


    // Update is called once per frame
    void Update()
    {
        vPlayerOrigin = PlayerObj.transform.position;

        Vector3 vFixedDistance = (transform.position - vPlayerOrigin).normalized * fRadius + vPlayerOrigin;

        transform.position = new Vector3(vFixedDistance.x, 1, vFixedDistance.z);

        //if (Input.GetKey("a"))
        //{
        //    transform.RotateAround(vPlayerOrigin, axis, rotationSpeed * Time.deltaTime);
        //}

        if (Input.GetKey("j"))
        {
            transform.RotateAround(vPlayerOrigin, axis, fRotationSpeed);
            
        }
        if (Input.GetKey("l"))
        {
            transform.RotateAround(vPlayerOrigin, axis, -fRotationSpeed);
            
        }

        if (Input.GetKeyUp("e"))
        {
            Vector3 vTeleLocation = new Vector3(transform.position.x, 1.22f, transform.position.z);

            PlayerObj.transform.position = vTeleLocation;
            //Needs to save rotation
            //transform.position = new Vector3(vTeleLocation.x + 4, vTeleLocation.y, vTeleLocation.z);
        }
        //transform.RotateAround(PlayerObj.transform.position, axis, rotationSpeed * Time.deltaTime);
        //var desiredPosition = (transform.position - PlayerObj.transform.position).normalized * fRadius + PlayerObj.transform.position;
        //transform.position = Vector3.MoveTowards(transform.position, desiredPosition, Time.deltaTime * radiusSpeed);
    }



    void ResetMandala() {
        vPlayerOrigin = PlayerObj.transform.position;

        transform.position = new Vector3(vPlayerOrigin.x + 4, vPlayerOrigin.y, vPlayerOrigin.z);
    }

}

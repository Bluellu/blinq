using UnityEngine;
using System.Collections;

public class TeleportationController : MonoBehaviour {
    public GameObject PlayerObj;

    public float fMovementRange = 10;
    public float fSpeed = 4.5f;
    public float fRadius =5;

    public bool canPlayerControl;

    public int nState;
    private float fSpacing;
    private Vector3 vPos;
    private Vector3 axis = Vector3.up;
    private Vector3 vPlayerOrigin, vMarkerPosition;
    public float fRotationSpeed = 2.0f;

    private GameObject playerObject;
    private PlayerController playerController;


    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;

    private float t = 0.0f;

    // Use this for initialization
    void Start () {
        startTime = Time.time;
        journeyLength = 50.0f;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerController = playerObject.GetComponent<PlayerController>();
        canPlayerControl = true;
        vPlayerOrigin = PlayerObj.transform.position;

        transform.position = new Vector3(vPlayerOrigin.x + 4, vPlayerOrigin.y, vPlayerOrigin.z);
        

        nState = 0;
        fRadius = 4.0f;

        fSpacing = 1.0f;


    }


    // Update is called once per frame
    void Update()
    {
        vPlayerOrigin = PlayerObj.transform.position;

        //state 0 is movement
        if (nState == 0)
        {
            if (canPlayerControl)
            {
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

                    ResetMandala();
                }
            }
        }
        //move player to telelocation
        else if (nState == 1)
        {

            if (t < 0.1f)
            {
                t += Time.deltaTime * 0.1f;
                playerObject.transform.position = Vector3.Lerp(vPlayerOrigin, vMarkerPosition, t);
                Debug.Log(t);
            }
            else
            {
                playerObject.transform.position = Vector3.Lerp(vPlayerOrigin, vMarkerPosition, 1);
                nState = 0;
                t = 0.0f;
                canPlayerControl = true;
                playerController.canPlayerControl = true;
                
            }

        }

    }



    void ResetMandala() {
        canPlayerControl = false;
        playerController.canPlayerControl = false;
        MeshRenderer modelRen = playerObject.GetComponentInChildren<MeshRenderer>();

        //modelRen.enabled = false;
        vMarkerPosition = transform.position;
        nState = 1;
        /*
        Vector3 vTeleLocation = new Vector3(transform.position.x, 1.22f, transform.position.z);

        PlayerObj.transform.position = vTeleLocation;
        
        vPlayerOrigin = PlayerObj.transform.position;
        

        transform.position = new Vector3(vPlayerOrigin.x + 4, vPlayerOrigin.y, vPlayerOrigin.z);
        */
    }

}

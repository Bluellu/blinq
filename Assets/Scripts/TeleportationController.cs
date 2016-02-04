using UnityEngine;
using System.Collections;

public class TeleportationController : MonoBehaviour {
    public GameObject PlayerObj;

    public bool canPlayerControl;

    public int nState;
    private Vector3 vPos;
    private Vector3 axis = Vector3.up;
    private Vector3 vPlayerOrigin, vMarkerPosition, vMarkerDirection;
    public float fRotationSpeed = 2.0f;

    private GameObject playerObject;
    private PlayerController playerController;
    
    public float speed = 1.0F;
    private float startTime;
    private float journeyLength;
    private float fMarkerDistance;

    private float fLerpingValue = 0.0f;

    // Use this for initialization
    void Start () {
        startTime = Time.time;
        journeyLength = 50.0f;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerController = playerObject.GetComponent<PlayerController>();
        canPlayerControl = true;
        vPlayerOrigin = PlayerObj.transform.position;

        transform.position = vPlayerOrigin + new Vector3(3, 0 ,3);
        

        nState = 0;



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
            transform.parent = null;
            if (fLerpingValue < 0.1f)
            {
                fLerpingValue += Time.deltaTime * 0.1f;
                playerObject.transform.position = Vector3.Lerp(vPlayerOrigin, vMarkerPosition, fLerpingValue);
                Debug.Log(fLerpingValue);
            }
            else
            {
                playerObject.transform.position = Vector3.Lerp(vPlayerOrigin, vMarkerPosition, 1);
                transform.parent = PlayerObj.transform;
                MeshRenderer modelRen = playerObject.GetComponentInChildren<MeshRenderer>();
                modelRen.enabled = true;
                transform.position = vPlayerOrigin + vMarkerDirection;

                nState = 0;
                fLerpingValue = 0.0f;
                canPlayerControl = true;
                playerController.canPlayerControl = true;
                
            }

        }

    }

    void ResetMandala() {
        canPlayerControl = false;
        playerController.canPlayerControl = false;

        vMarkerDirection = transform.position - playerObject.transform.position;
        fMarkerDistance = Vector3.Distance(vMarkerDirection, playerObject.transform.position);
        MeshRenderer modelRen = playerObject.GetComponentInChildren<MeshRenderer>();
        
        modelRen.enabled = false;
        vMarkerPosition = transform.position;
        nState = 1;

    }

}

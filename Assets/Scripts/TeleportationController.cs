using UnityEngine;
using System.Collections;

public class TeleportationController : MonoBehaviour {
    public GameObject PlayerObj;
    MeshRenderer modelRen;

    public bool canPlayerControl;
	public bool canTeleport;
	public bool onMag;

    public int nState;
    private Vector3 vPos;
    private Vector3 axis = Vector3.up;
    private Vector3 vPlayerOrigin, vMarkerPosition, vMarkerDirection, vSavedDestination, vPlayerOriginEnd, vPlayerOriginStart;
    public float fRotationSpeed = 2.0f;

    private GameObject playerObject;
    private PlayerController playerController;
  

    private float fLerpingValue;
    

    // Use this for initialization
    void Start () {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerController = playerObject.GetComponent<PlayerController>();
        canPlayerControl = true;
		canTeleport = true;
		onMag = false;
        vPlayerOrigin = PlayerObj.transform.position;

        modelRen = playerObject.GetComponentInChildren<MeshRenderer>();
        fLerpingValue = 0.0f;
        nState = 0;
        
		setRadius(new Vector3(4, 0 ,4));
    }


    // Update is called once per frame
    void FixedUpdate()
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

				if (Input.GetKeyUp("e") && canTeleport)
                {

                    ResetMandala();
                }
            }
        }
        //move player to telelocation
        else if (nState == 1)
        {
            transform.parent = null;

            if (LerpingTranslate(vPlayerOriginEnd, vMarkerPosition, playerObject))
            {
                vSavedDestination = vPlayerOriginStart + vMarkerDirection;
                vPlayerOriginEnd = vMarkerPosition;
                nState = 2;
            }
        }
        //Move marker to saved angle location
        else if (nState == 2)
        {
            if (LerpingTranslate(vPlayerOriginEnd, vSavedDestination, transform.root.gameObject))
            {
                nState = 0;
                transform.parent = PlayerObj.transform;
                modelRen.enabled = true;
                canPlayerControl = true;
                playerController.canPlayerControl = true;
            }
        }
    }

    void ResetMandala() {
        canPlayerControl = false;
        playerController.canPlayerControl = false;
        vMarkerDirection = transform.position - playerObject.transform.position;
        modelRen.enabled = false;
        vMarkerPosition = transform.position;
        vPlayerOriginEnd = PlayerObj.transform.position;
        vPlayerOriginStart = transform.position;
        nState = 1;

    }

    bool LerpingTranslate(Vector3 vStart, Vector3 vEnd, GameObject goToMove) {
        //Debug.Log(fLerpingValue);
        if (fLerpingValue < 1.0f)
        {
            fLerpingValue += Time.deltaTime * 2.4f;
            goToMove.transform.position = Vector3.Lerp(vStart, vEnd, fLerpingValue);
        }
        else
        {
            goToMove.transform.position = Vector3.Lerp(vStart, vEnd, 1);
            fLerpingValue = 0.0f;
            return true;
        }
        return false;
    }

	public void setRadius(Vector3 pos) {

		transform.position = vPlayerOrigin + pos;
		
	}

	public Vector3 getPlayerOrigin() 
	{
		return vPlayerOrigin;
	}

	public void increaseRadius() {
		if (!onMag) {
			Vector3 origin = getPlayerOrigin (); // Player's position
			Vector3 distance = transform.position - origin; // Distance of mandala from a player
			Vector3 new_distance = new Vector3 (distance.x * 2, distance.y, distance.z * 2); // Extend the distance
			setRadius (new_distance); // Set radius
		}
		onMag = true;
	}
}

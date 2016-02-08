﻿using UnityEngine;
using System.Collections;

public class TeleportationController : MonoBehaviour {
    public GameObject PlayerObj;
    SkinnedMeshRenderer[] modelRen; 

    public bool canPlayerControl;
	public bool canTeleport;
	public bool onMag;
    public bool active;

    public int nState;
    private Vector3 axis = Vector3.up;
    private Vector3 vPlayerOrigin, vMarkerPosition, vMarkerDirection, vSavedDestination, vPlayerOriginEnd, vPlayerOriginStart;
    public float fRotationSpeed;
    
    private GameObject playerObject;
    private PlayerController playerController;
    public float fTeleHeight, fMandelaHeight;

    private float fLerpingValue;
    private bool bFirstTime;
    private float timeBetweenActivation = 0.5f;
    private float timestamp;

    // Use this for initialization
    void Start () {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerController = playerObject.GetComponent<PlayerController>();
        canPlayerControl = true;
        fTeleHeight = 1.0f;

        vPlayerOrigin = PlayerObj.transform.position;
        fMandelaHeight = vPlayerOrigin.y + 10;

		canTeleport = false;
		onMag = false;
        active = false;
        bFirstTime = true;

        fLerpingValue = 0.0f;
        fRotationSpeed = 4.0f;
        nState = 0;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (canTeleport)
        {
            vPlayerOrigin = PlayerObj.transform.position;

            //state 0 is movement
            if (nState == 0)
            {
                if (bFirstTime)
                {
                    transform.position = new Vector3(transform.position.x, fMandelaHeight + 1, transform.position.z);
                    
                }
                else
                {
                    transform.position = new Vector3(transform.position.x, fMandelaHeight + 1, transform.position.z);
                }
                if (canPlayerControl)
                {
                    if (Mathf.Abs(Input.GetAxis("RightH")) == 1 || Mathf.Abs(Input.GetAxis("RightV")) == 1)
                    {
                        float distH = Input.GetAxis("RightH") * 6.5f;
                        float distV = Input.GetAxis("RightV") * 6.5f;

                        Vector3 vNewpos = new Vector3(distH, 0, -distV);
                        
                        
                        transform.position = vPlayerOrigin + vNewpos;

                      
                    }

                    if (Input.GetKey("i") && Time.time >= timestamp)
                    {
                        if (!active)
                        {
                            Vector3 vNewpos = Vector3.left * 6.5f;
                            transform.position = vPlayerOrigin + vNewpos;
                            active = true;
                        } else if (active)
                        {
                            transform.position = vPlayerOrigin;
                            active = false;
                        }
                        timestamp = Time.time + timeBetweenActivation;
                    }

                    



                    if (Input.GetKey("j") || Input.GetKey("joystick button 1"))
                    {
                        transform.RotateAround(vPlayerOrigin, axis, fRotationSpeed);

                    }
                    if (Input.GetKey("l") || Input.GetKey("joystick button 2"))
                    {
                        transform.RotateAround(vPlayerOrigin, axis, -fRotationSpeed);
                    }

                    if (Input.GetButton("Teleport"))
                    {
                        bFirstTime = false;
                        ResetMandala();
                    }
                    
                }
            }
            //move player to telelocation
            else if (nState == 1)
            {

                if (LerpingTranslate(vPlayerOriginEnd, new Vector3(vMarkerPosition.x, fTeleHeight, vMarkerPosition.z), playerObject))
                {
                    vPlayerOriginEnd = vMarkerPosition;
                    nState = 2;
                }
            }
            //Move marker to saved angle location
            else if (nState == 2)
            {
                RenderPlayerModel(true);

                if (LerpingTranslate(new Vector3(vPlayerOriginEnd.x, fMandelaHeight, vPlayerOriginEnd.z), new Vector3(vSavedDestination.x, fMandelaHeight, vSavedDestination.z), gameObject))
                {
                    nState = 0;
                    transform.parent = PlayerObj.transform;
                    canPlayerControl = true;
                    playerController.canPlayerControl = true;
                }
            }
        }
    }

    Vector3 changeSavedDestination(Vector3 savedDestination)
    {
        float x = (savedDestination.x - vPlayerOrigin.x) / 2;
        float z = (savedDestination.z - vPlayerOrigin.z) / 2;

        return vPlayerOrigin + new Vector3(x, fMandelaHeight, z);
    }
    void ResetMandala() {
        transform.parent = null;
        canPlayerControl = false;
        playerController.canPlayerControl = false;
        vMarkerDirection = transform.position - playerObject.transform.position;
        RenderPlayerModel(false);
        vMarkerPosition = transform.position;
        vPlayerOriginEnd = PlayerObj.transform.position;
        vPlayerOriginStart = transform.position;
        nState = 1;
        fTeleHeight = fMandelaHeight;
        if (onMag)
        {
            Debug.Log("ONMAG");
            vSavedDestination = vPlayerOriginStart + new Vector3(vMarkerDirection.x / 2, fTeleHeight, vMarkerDirection.z / 2);
            onMag = false;
        }
        else
        {
            Debug.Log("!ONMAG");
            vSavedDestination = vPlayerOriginStart + new Vector3(vMarkerDirection.x, fTeleHeight, vMarkerDirection.z);
        }
        Debug.Log(vSavedDestination);

    }

    bool LerpingTranslate(Vector3 vStart, Vector3 vEnd, GameObject goToMove) {

        //Debug.Log(fLerpingValue);
        if (fLerpingValue < 1.0f)
        {
            fLerpingValue += Time.deltaTime * 5.4f;
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

    public void ChangeMandalaHeight(float fAmount)
    {
        fMandelaHeight = fAmount;

    }

	public void setRadius(Vector3 pos) {

		transform.position = vPlayerOrigin + new Vector3(pos.x, 0, pos.z);
		
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
	}
    public void RenderPlayerModel(bool bSwitch)
    {
        modelRen = PlayerObj.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer ren in modelRen)
        {
            ren.enabled = bSwitch;
        }

    }

    

}

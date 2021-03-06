﻿using UnityEngine;
using System.Collections;

public class MandalaMovementController : MonoBehaviour {

    public bool canPlayerControl;
    public bool canTeleport;
    public bool isAttached;
    public GameObject MandalaObject;
    private Vector3 axis = Vector3.up;

    public float fRotationSpeed;

    private GameObject playerObject, Mandala;
    //private PlayerController playerController;
    private TeleportationController teleportationController;
    public float fTeleHeight, fMandelaHeight, fLerpingValue;

    public Vector3 targetDirection;
    public Quaternion targetRotation;

    public float fRadius;

    private bool bReached;
    private float nMovingState, fMovingSpeed;

    // Use this for initialization
    void Start () {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        Mandala = GameObject.FindGameObjectWithTag("Mandala");
        //playerController = playerObject.GetComponent<PlayerController>();
        teleportationController = Mandala.GetComponent<TeleportationController>();

        canTeleport = false;
        canPlayerControl = true;

        if (isAttached)
        {
            canTeleport = true;
        }
        fLerpingValue = 0.0f;


        if (canTeleport)
        {
            AddMandalaRadius(fRadius);
           
        }
        fMandelaHeight = transform.position.y;
        nMovingState = 0;
        fMovingSpeed = 400f;
        bReached = false;

    }

    // Update is called once per frame
    void Update()
    {
        teleportationController.fRadius = fRadius;
        teleportationController.targetRotation = transform.rotation;

        if(teleportationController.nState == 0 && canTeleport)
            AddMandalaRadius(fRadius);

        if (canTeleport)
        {
            targetDirection = new Vector3(Input.GetAxis("RightH"), 0f, -Input.GetAxis("RightV"));

            if (targetDirection.magnitude > 0.9f)
                targetRotation = Quaternion.LookRotation(new Vector3(Input.GetAxis("RightH"), 0f, -Input.GetAxis("RightV")));
            else
                nMovingState = 1;

            MandalaObject.transform.position = new Vector3(MandalaObject.transform.position.x, fMandelaHeight, MandalaObject.transform.position.z);

            if(nMovingState == 0)
            {
                if ((Quaternion.Angle(transform.rotation, targetRotation) < 2))
                {
                    nMovingState = 1;
                }
                fMovingSpeed = 500;
            }
            if (nMovingState == 1)
            {
                if (Quaternion.Angle(transform.rotation, targetRotation) > 150)
                {
                    nMovingState = 0;
                }
                fMovingSpeed = 150;
            }
            
            if (targetDirection.magnitude > 0.9f)
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, Time.deltaTime * fMovingSpeed);
                }         

            if (Input.GetKey("j") || Input.GetKey("joystick button 1"))
            {
                Debug.Log("pressed");
                transform.RotateAround(playerObject.transform.position, axis, 2);

            }
            if (Input.GetKey("l") || Input.GetKey("joystick button 2"))
            {
                transform.RotateAround(playerObject.transform.position, axis, -2);
            }

        }
    }

    public void ChangeMandalaHeight(float fAmount)
    {
        fMandelaHeight = fAmount;
    }

    public void AddMandalaRadius(float fRadius)
    {
        float fDistance = Vector3.Distance(MandalaObject.transform.position, transform.position);
        
        while(fDistance <= fRadius)
        {
            fDistance = Vector3.Distance(MandalaObject.transform.position , transform.position);
            MandalaObject.transform.Translate(Vector3.forward * Time.deltaTime, Space.Self);

        }
            
    }
    public void SubMandalaRadius(float fRadius)
    {

        MandalaObject.transform.Translate(Vector3.back * fRadius, Space.Self);
    }


}

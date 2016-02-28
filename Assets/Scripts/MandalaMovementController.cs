using UnityEngine;
using System.Collections;

public class MandalaMovementController : MonoBehaviour {

    public bool canPlayerControl;
    public bool canTeleport;
    public bool isAttached;
    public GameObject MandalaObject;
    private Vector3 axis = Vector3.up;
    private Vector3 vPlayerOrigin;
    public float fRotationSpeed;

    private GameObject playerObject;
    private PlayerController playerController;
    public float fTeleHeight, fMandelaHeight, fLerpingValue;


    private bool  moved, movingMandala;
    public float fRadius;

    private Vector3 vCurDirection, vPrevDirection;
    // Use this for initialization
    void Start () {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerController = playerObject.GetComponent<PlayerController>();

        canTeleport = false;
        canPlayerControl = true;

        if (isAttached)
        {
            canTeleport = true;
        }
        fLerpingValue = 0.0f;
        moved = false;
        //fMandelaHeight = vPlayerOrigin.y + 10;
        movingMandala = false;

        if (canTeleport)
        {
            MandalaObject.transform.Translate(Vector3.forward * fRadius, Space.Self);
        }
        fMandelaHeight = transform.position.y;

    }

    // Update is called once per frame
    void Update()
    {
        if (canTeleport)
        {
            Vector3 targetDirection = new Vector3(Input.GetAxis("RightH"), 0f, -Input.GetAxis("RightV"));

            MandalaObject.transform.position = new Vector3(MandalaObject.transform.position.x, fMandelaHeight, MandalaObject.transform.position.z);

            if (Mathf.Abs(Input.GetAxis("RightH")) == 1 || Mathf.Abs(Input.GetAxis("RightV")) == 1)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10);
            }

            if (Input.GetKey("j") || Input.GetKey("joystick button 1"))
            {
                transform.RotateAround(playerObject.transform.position, axis, fRotationSpeed);

            }
            if (Input.GetKey("l") || Input.GetKey("joystick button 2"))
            {
                transform.RotateAround(playerObject.transform.position, axis, -fRotationSpeed);
            }

        }
    }

    public void ChangeMandalaHeight(float fAmount)
    {
        fMandelaHeight = fAmount;
    }


}

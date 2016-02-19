using UnityEngine;
using System.Collections;

public class MandalaMovementController : MonoBehaviour {

    public bool canPlayerControl;
    public bool canTeleport;

    private Vector3 axis = Vector3.up;
    private Vector3 vPlayerOrigin;
    public float fRotationSpeed;

    private GameObject playerObject;
    private PlayerController playerController;
    public float fTeleHeight, fMandelaHeight;

    public bool bFirstTime;
    private float timeBetweenActivation = 0.5f;
    private float timestamp;
    private bool movedToPosition, moved;

    private Vector3 vCurDirection, vPrevDirection;
    // Use this for initialization
    void Start () {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerController = playerObject.GetComponent<PlayerController>();
        movedToPosition = false;
        canTeleport = false;
        canPlayerControl = true;
        bFirstTime = true;
        moved = false;
        fMandelaHeight = vPlayerOrigin.y + 10;
    }

    // Update is called once per frame
    void Update()
    {
        if (canTeleport)
        {
            Vector3 targetDirection = new Vector3(Input.GetAxis("RightH"), 0f, -Input.GetAxis("RightV"));

            vCurDirection = targetDirection; ;

            if (Mathf.Abs(Input.GetAxis("RightH")) == 1 || Mathf.Abs(Input.GetAxis("RightV")) == 1)
            {
                if (!moved)
                {
                    transform.position = transform.position + targetDirection * 5;
                    moved = true;
                }
                else
                {

                }

                //if (vCurDirection != vPrevDirection)
                //{
                //    Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

                //    transform.rotation = targetRotation;
                //    if (Vector3.Magnitude(playerObject.transform.position - transform.position) < 5)
                //    {
                        
                //        //transform.Translate(Vector3.forward, Space.Self);

                //    }
                //    else
                //    {
                //        vPrevDirection = targetDirection;
                //    }

                //}

            }
            else {
                transform.position = playerObject.transform.position;

            }
            

            
            
            


            //vPlayerOrigin = playerObject.transform.position;
            //if (bFirstTime)
            //{
            //    transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            //}
            //else
            //{
            //    transform.position = new Vector3(transform.position.x, fMandelaHeight + 1, transform.position.z);
            //}

            //if (canPlayerControl)
            //{
            //    if (Mathf.Abs(Input.GetAxis("RightH")) == 1 || Mathf.Abs(Input.GetAxis("RightV")) == 1)
            //    {
            //        float distH = Input.GetAxis("RightH") * 6.5f;
            //        float distV = Input.GetAxis("RightV") * 6.5f;

            //        Vector3 vNewpos = new Vector3(distH, 0, -distV);
            //        transform.position = vPlayerOrigin + vNewpos;
            //    }

            //    if (Input.GetKey("i") && Time.time >= timestamp)
            //    {
            //        Vector3 vNewpos = Vector3.left * 6.5f;
            //        transform.position = vPlayerOrigin + vNewpos;

            //        timestamp = Time.time + timeBetweenActivation;
            //    }

            //    if (Input.GetKey("j") || Input.GetKey("joystick button 1"))
            //    {
            //        transform.RotateAround(vPlayerOrigin, axis, fRotationSpeed);

            //    }
            //    if (Input.GetKey("l") || Input.GetKey("joystick button 2"))
            //    {
            //        transform.RotateAround(vPlayerOrigin, axis, -fRotationSpeed);
            //    }

            //}
        }
    }

    public void ChangeMandalaHeight(float fAmount)
    {
        fMandelaHeight = fAmount;

    }
}

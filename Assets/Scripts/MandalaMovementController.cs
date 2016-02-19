using UnityEngine;
using System.Collections;

public class MandalaMovementController : MonoBehaviour {

    public bool canPlayerControl;
    public bool canTeleport;
    public GameObject MandalaMarker;
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
        fLerpingValue = 0.0f;
        moved = false;
        fMandelaHeight = vPlayerOrigin.y + 10;
        movingMandala = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (canTeleport)
        {
            Vector3 targetDirection = new Vector3(Input.GetAxis("RightH"), 0f, -Input.GetAxis("RightV"));

            vCurDirection = targetDirection; 

            if (Mathf.Abs(Input.GetAxis("RightH")) == 1 || Mathf.Abs(Input.GetAxis("RightV")) == 1)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
                transform.rotation = targetRotation;                

                if (Vector3.Magnitude(MandalaMarker.transform.position - transform.position) < fRadius)
                {
                    MandalaMarker.transform.Translate(Vector3.forward / 2, Space.Self);
                    Debug.Log(fMandelaHeight);
                    
                }
                MandalaMarker.transform.position = new Vector3(MandalaMarker.transform.position.x, fMandelaHeight, MandalaMarker.transform.position.z);

            }
            else
            {
                MandalaMarker.transform.position = playerObject.transform.position;
                moved = false;
            }
        }
    }

    public void ChangeMandalaHeight(float fAmount)
    {
        fMandelaHeight = fAmount;

    }


}

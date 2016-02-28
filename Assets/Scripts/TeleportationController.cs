using UnityEngine;
using System.Collections;

public class TeleportationController : MonoBehaviour {
    public GameObject PlayerObj;
    public GameObject MarkerObj;
    SkinnedMeshRenderer[] modelRen; 

    public bool canPlayerControl;
	public bool canTeleport, canActivateTele;
    public bool isAttached;

    public GameObject Particles;

    
    public int nState;
    private Vector3 vPlayerOrigin, 
                    vMarkerPosition, 
                    vMarkerDirection, 
                    vPlayerOriginEnd, 
                    vPlayerOriginStart,
                    vSavedDestination;
    public float fRotationSpeed;
    
    private GameObject playerObject;
    private PlayerController playerController;
    public float fTeleHeight, fMandelaHeight;

    private float fLerpingValue;


    private MandalaMovementController mandalaMovementController;



    // Use this for initialization
    void Start () {
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerController = playerObject.GetComponent<PlayerController>();
        canPlayerControl = true;
        fTeleHeight = 1.0f;

        vPlayerOrigin = PlayerObj.transform.position;
        fMandelaHeight = vPlayerOrigin.y + 10;

		canTeleport = false;
        canActivateTele = true;
        if (isAttached)
        {
            canTeleport = true;
        }


        fLerpingValue = 0.0f;
        fRotationSpeed = 4.0f;
        nState = 0;

        mandalaMovementController = transform.GetComponentInParent<MandalaMovementController>();

        
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (canTeleport)
        {
            //vPlayerOrigin = PlayerObj.transform.position;

            //state 0 is movement
            if (nState == 0)
            {
                
                if (Input.GetButton("Teleport") && canActivateTele)
                {
                    //Particles.transform.position = PlayerObj.transform.position;
                    Instantiate(Particles, PlayerObj.transform.position, new Quaternion(0, 0, 0, 90));
                    
                    ResetMandala();
                }                    

            }
            //move player to telelocation
            else if (nState == 1)
            {
                
                if (LerpingTranslate(vPlayerOriginEnd, new Vector3(vMarkerPosition.x, fTeleHeight, vMarkerPosition.z), playerObject))
                {
                    vPlayerOriginEnd = vMarkerPosition;
                    nState = 2;
                    Instantiate(Particles, transform.position, new Quaternion(0, 0, 0, 90));
                    vSavedDestination = vPlayerOriginStart + new Vector3(vMarkerDirection.x, fTeleHeight, vMarkerDirection.z);

                }
            }
            //Move marker to saved angle location
            else if (nState == 2)
            {
                if (LerpingTranslate(new Vector3(vPlayerOriginEnd.x, fMandelaHeight, vPlayerOriginEnd.z), new Vector3(vSavedDestination.x, fMandelaHeight, vSavedDestination.z), gameObject))
                {
                    mandalaMovementController.canTeleport = true;
                    RenderPlayerModel(true);
                    nState = 0;
                    transform.parent = MarkerObj.transform;
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
        mandalaMovementController.canTeleport = false;
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

    }

    bool LerpingTranslate(Vector3 vStart, Vector3 vEnd, GameObject goToMove) {
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





    public void RenderPlayerModel(bool bSwitch)
    {
        modelRen = PlayerObj.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer ren in modelRen)
        {
            ren.enabled = bSwitch;
        }

    }

    

}

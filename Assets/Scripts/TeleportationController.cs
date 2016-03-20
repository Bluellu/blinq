using UnityEngine;
using System.Collections;

public class TeleportationController : MonoBehaviour {
    public GameObject PlayerObj;
    public GameObject MarkerObj;
    SkinnedMeshRenderer[] modelRen; 

    public bool canPlayerControl;
	public bool canTeleport, canActivateTele;
    public bool isAttached;

    public GameObject Particles, Particles2, Particles3;
    public Quaternion targetRotation;

    public int nState;

    public bool onRelocationTile;

    public Vector3 vRelocationTilePos;
    private Vector3 vPlayerOrigin,
                    vMarkerPosition,
                    vPlayerOriginEnd,                     
                    vSavedDestination;
    public float fRotationSpeed;
    
    private GameObject playerObject;
    private PlayerController playerController;
    public float fTeleHeight, fMandelaHeight;

    private float fLerpingValue;

    public float fRadius;
    private MandalaMovementController mandalaMovementController;

    public bool MandalaInAir;
    public bool InAir;



    // Use this for initialization
    void Start () {
        InAir = false;
        playerObject = GameObject.FindGameObjectWithTag("Player");
        playerController = playerObject.GetComponent<PlayerController>();
        canPlayerControl = true;
        fTeleHeight = 1.0f;
        onRelocationTile = false;
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
            
            //state 0 is movement
            if (nState == 0)
            {
                if ((Input.GetButton("Teleport") && canActivateTele) || (Input.GetKey("e") && canActivateTele))
                {

                    RaycastHit hit_below;
                    if (Physics.Raycast(PlayerObj.transform.position, Vector3.down, out hit_below, Mathf.Infinity))
                    {
                        if (hit_below.collider.tag != "Floor")
                        {
                            Debug.Log("hittelepor");
                            Instantiate(Particles, PlayerObj.transform.position, new Quaternion(0, 0, 0, 90));
                            Instantiate(Particles3, PlayerObj.transform.position, new Quaternion(0, 0, 0, 90));
                            ResetMandala();
                        }                    

                    }
                    if(!MandalaInAir)
                    {
                        Instantiate(Particles, PlayerObj.transform.position, new Quaternion(0, 0, 0, 90));
                        Instantiate(Particles3, PlayerObj.transform.position, new Quaternion(0, 0, 0, 90));
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
                    Instantiate(Particles, transform.position, new Quaternion(0, 0, 0, 90));
					Instantiate(Particles2, transform.position, new Quaternion(0, 0, 0, 90));

                    GameObject newStuff = new GameObject();
                    newStuff.transform.position = PlayerObj.transform.position;
                    newStuff.transform.rotation = targetRotation;
                    newStuff.transform.Translate(Vector3.forward * fRadius, Space.Self);
                    vSavedDestination = newStuff.transform.position;
                    if (MandalaInAir)
                        InAir = true;
                    
                        

                    
                    Destroy(newStuff);

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
        RenderPlayerModel(false);
        if(onRelocationTile)
            {
            vMarkerPosition = vRelocationTilePos;
            }
        else {
            vMarkerPosition = transform.position;
        }
        
        vPlayerOriginEnd = PlayerObj.transform.position;
        
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

using UnityEngine;
using System.Collections;

public class MarkerCollisionController : MonoBehaviour {

    private float distanceToGround;

    public Transform playerObj;

    private TeleportationController teleportationController;
    private MandalaMovementController manMoveController;

    public bool onDisableTile;
    public bool InAir;
    Vector3 vAdjustedOrigin;
    public float fYvalueRay;
    
    // Use this for initialization
    void Start () {
        onDisableTile = false;
        teleportationController = gameObject.GetComponent<TeleportationController>();
        manMoveController = gameObject.GetComponentInParent<MandalaMovementController>();
        
        distanceToGround = 0;
        fYvalueRay = 100;
        vAdjustedOrigin = new Vector3(transform.position.x, fYvalueRay, transform.position.z);
    }
	
	// Update is called once per frame
	void FixedUpdate () {

        vAdjustedOrigin = new Vector3(transform.position.x, fYvalueRay, transform.position.z);

        RaycastHit hit_below;

        Debug.DrawRay(vAdjustedOrigin, Vector3.down, new Color(0,1, 0));

        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        if (Physics.Raycast(vAdjustedOrigin, Vector3.down, out hit_below, Mathf.Infinity, layerMask))
        {
            if (hit_below.collider.tag == "LevelModel")
            {                
                distanceToGround = hit_below.point.y;
                manMoveController.ChangeMandalaHeight(distanceToGround);
                teleportationController.MandalaInAir = false;
                teleportationController.ChangeMandalaHeight(distanceToGround);
                
                if (!onDisableTile)
                    teleportationController.canActivateTele = true;
            }
            else {
                
                manMoveController.ChangeMandalaHeight(playerObj.position.y);
                teleportationController.ChangeMandalaHeight(playerObj.position.y);
                teleportationController.MandalaInAir = true;
            }
            
        }
    }
}

using UnityEngine;
using System.Collections;

public class MarkerCollisionController : MonoBehaviour {

    private float distanceToGround, distanceToGroundInside;

    public Transform playerObj;

    private TeleportationController teleController;

    Vector3 vAdjustedOrigin;
    public float fYvalueRay;
    
    // Use this for initialization
    void Start () {
        teleController = gameObject.GetComponent<TeleportationController>();
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


                teleController.ChangeMandalaHeight(distanceToGround);

            }
            else {
                teleController.ChangeMandalaHeight(playerObj.position.y);
            }
            
        }

       






    }

}

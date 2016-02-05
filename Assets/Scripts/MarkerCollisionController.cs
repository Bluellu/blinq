using UnityEngine;
using System.Collections;

public class MarkerCollisionController : MonoBehaviour {

    private float distanceToGround, distanceToGroundInside;

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
        Ray rRay = new Ray(vAdjustedOrigin, Vector3.down);

        Debug.DrawRay(vAdjustedOrigin, Vector3.down, new Color(0,1, 0));

        int layerMask = 1 << 8;
        layerMask = ~layerMask;

        if (Physics.Raycast(vAdjustedOrigin, Vector3.down, out hit_below, Mathf.Infinity, layerMask))
        {

            distanceToGround = hit_below.point.y;

            Debug.Log(distanceToGround);
            
            if (hit_below.collider.tag == "LevelModel")
            {

                distanceToGround = hit_below.point.y;

                Debug.Log(distanceToGround);

                teleController.ChangeMandalaHeight(distanceToGround);


            }
            
        }

       






    }

}

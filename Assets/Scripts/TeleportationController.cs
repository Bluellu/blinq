using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TeleportationController : MonoBehaviour {
    public GameObject PlayerObj;
    public GameObject TeleportationObj;

    public Text rangeText;
    public float fMovementRange = 10;
    public float fSpeed = 4.5f;
    private float fSpacing = 1.0f;
    private Vector3 vPos;
    private Vector3 vDistance;


    // Use this for initialization
    void Start () {
        InvokeRepeating("GetRange", 1, 1);

        rangeText.text = "";


    }
	
	// Update is called once per frame
	void Update () {
        
        Vector3 vDistance = TeleportationObj.transform.position - PlayerObj.transform.position;
        
        Vector3 vClampDist = vDistance;
        vClampDist.x = Mathf.Clamp(vClampDist.x , PlayerObj.transform.position.x - 2.2f, PlayerObj.transform.position.x + 2.2f);
        vClampDist.z = Mathf.Clamp(vClampDist.z , PlayerObj.transform.position.z - 2.2f, PlayerObj.transform.position.z + 2.2f);
        //transform.position = vClampDist;
        if (vDistance.magnitude > 3)
        {
            TeleportationObj.transform.position = vClampDist;
        }
        

        Debug.DrawLine(PlayerObj.transform.position, TeleportationObj.transform.position, new Color(1, 0 ,0));
        //Debug.Log(vDistance.magnitude);
        //if (vDistance.sqrMagnitude < fMovementRange)
        //{
            vPos = new Vector3(0, 0, 0);
            //Move teleportation object 
            if (Input.GetKey("i"))
            {
                vPos.z += fSpacing;
            }
            if (Input.GetKey("j"))
            {
                vPos.x -= fSpacing;
            }
            if (Input.GetKey("k"))
            {
                vPos.z -= fSpacing;
            }
            if (Input.GetKey("l"))
            {
                vPos.x += fSpacing;
            }
            if (vDistance.magnitude > 5)
            {
                
            }
            TeleportationObj.transform.Translate(vPos * fSpeed * Time.deltaTime);
      

        //Teleport player to object
        if (Input.GetKey("e"))
        {
            Vector3 vTeleLocation = new Vector3(TeleportationObj.transform.position.x, 0, TeleportationObj.transform.position.z);
            PlayerObj.transform.position = vTeleLocation;
        }

    }


    void GetRange()
    {
        vDistance = TeleportationObj.transform.position - PlayerObj.transform.position;
        rangeText.text = "Range to Player: " + vDistance.sqrMagnitude.ToString();
    }
}

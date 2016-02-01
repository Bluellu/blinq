using UnityEngine;
using System.Collections;

public class TeleportationController : MonoBehaviour {
    public GameObject PlayerObj;
    public GameObject TeleportationObj;

    public float fMovementRange = 10;
    public float fSpeed = 4.5f;
    private float fSpacing = 1.0f;
    private Vector3 vPos;
    private Vector3 vPosTrans;
    private Vector3 vDistance;

    private float posx, posy, posz;

    //State 0, tele marker follows
    public int nState;
    private float fDistance;
    private Vector3 vPlayerPos;
    private float xoffset, yoffset, zoffset;

    private bool bApplyOffset;

    // Use this for initialization
    void Start () {
        nState = 3;
        fDistance = 1.0f;
        //initialize teleportation marker
        vPlayerPos = PlayerObj.transform.position;
        vPlayerPos.y += .3f;
        Vector3 vZdis = Vector3.forward;
        TeleportationObj.transform.position = vPlayerPos - vZdis * fDistance;
        bApplyOffset = true;
        vPosTrans = TeleportationObj.transform.position;
    }


    // Update is called once per frame
    void Update()
    {
        ApplyMovement();
        //vPosTrans = PlayerObj.transform.position - TeleportationObj.transform.position;
        //if deer moves
        Debug.Log(nState);
        if (nState == 2)
        {
            bApplyOffset = false;
            vPlayerPos = PlayerObj.transform.position;
            vPlayerPos.y += .3f;
            /*
            TeleportationObj.transform.position = new Vector3(
                vPlayerPos.x + xoffset,
                vPlayerPos.y + yoffset,
                vPlayerPos.z + zoffset);
                */

        }
        else if (nState == 3)
        {
            
            fDistance = Vector3.Magnitude(TeleportationObj.transform.position - PlayerObj.transform.position);
            if (fDistance > 3)
            {
                nState = 2;
                if (bApplyOffset)
                {
                    xoffset = vPosTrans.x - vPlayerPos.x;
                    yoffset = vPosTrans.y - vPlayerPos.y;
                    zoffset = vPosTrans.z - vPlayerPos.z;
                    
                }

            }
            else
            {
                bApplyOffset = true;
                TeleportationObj.transform.Translate(vPos * fSpeed * Time.deltaTime);
                vPlayerPos = PlayerObj.transform.position;
                vPlayerPos.y += .3f;
                fDistance = Vector3.Magnitude(TeleportationObj.transform.position - vPlayerPos);
                if (fDistance > 3)
                {
                    TeleportationObj.transform.Translate(-vPos * fSpeed * Time.deltaTime);
                    nState = 2;
                    if (bApplyOffset)
                    {
                        xoffset = vPosTrans.x - vPlayerPos.x;
                        yoffset = vPosTrans.y - vPlayerPos.y;
                        zoffset = vPosTrans.z - vPlayerPos.z;
                    }
                    //Vector3 vZdis = vPlayerPos - vPosTrans;
                }
            }


        }
    }

    void ApplyMovement()
    {
        vPos = new Vector3(0, 0, 0);
        if (Input.GetKey("i"))
        {
            nState = 3;
            vPos.z += fSpacing;
        }
        if (Input.GetKey("j"))
        {
            nState = 3;
            vPos.x -= fSpacing;
        }
        if (Input.GetKey("k"))
        {
            nState = 3;
            vPos.z -= fSpacing;
        }
        if (Input.GetKey("l"))
        {
            nState = 3;
            vPos.x += fSpacing;
        }
        if (Input.GetKey("e"))
        {
            Vector3 vTeleLocation = new Vector3(TeleportationObj.transform.position.x, 0, TeleportationObj.transform.position.z);
            PlayerObj.transform.position = vTeleLocation;
            RefreshValues();
        }
    }

    void RefreshValues() {
        xoffset = 0;
        yoffset = 0;
        zoffset = 0;
        fDistance = 0;
        vPos = new Vector3(0, 0, 0);
        nState = 3;
    }

}

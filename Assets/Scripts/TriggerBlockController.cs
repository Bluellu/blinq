using UnityEngine;
using System.Collections;

public class TriggerBlockController : MonoBehaviour {

    public GameObject objectTotrigger;
    public GameObject ObjectTomove;

    private bool bMovingDown, bMovingUp;
    private float fAmount, fIncrement;
    private TriggeredTileMovement TTM;
    // Use this for initialization
    void Start () {
        bMovingDown = false;
        bMovingUp = false;
        fAmount = 0.5f;
        fIncrement = 0.0f;
        TTM = ObjectTomove.GetComponent<TriggeredTileMovement>();
    }
	
	// Update is called once per frame
	void Update () {
        if (bMovingDown)
        {
            if (fIncrement < fAmount)
            {
                objectTotrigger.transform.Translate(Vector3.down * Time.deltaTime);
                fIncrement += Time.deltaTime;
            }
            else
                TTM.on = true;

        }
        else if (bMovingUp)
        {

            if (fIncrement > 0)
            {
                objectTotrigger.transform.Translate(Vector3.up * Time.deltaTime);
                fIncrement -= Time.deltaTime;
            }
            else
            {
                TTM.on = false;
            }
            
                
        }
	}

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {

            bMovingDown = true;
            bMovingUp = false;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            bMovingDown = false;
            bMovingUp = true;

        }
    }
}

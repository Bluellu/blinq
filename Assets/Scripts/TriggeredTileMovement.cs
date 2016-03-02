using UnityEngine;
using System.Collections;

public class TriggeredTileMovement : MonoBehaviour {

    public GameObject toObject;
    private Vector3 origPoint;
    float distance;
    public float fSpeed;
    public bool on;
    bool reached = false;

    public void Start()
    {
        origPoint = transform.position;
        on = false;

    }

    public void FixedUpdate()
    {
        Debug.Log(Vector3.Distance(transform.position, toObject.transform.position));
        if(on)
            move(transform.position, toObject.transform.position);
        else
            move(transform.position, origPoint);

            
        
    }

    void move(Vector3 pos, Vector3 towards)
    {
        Vector3 direction = (towards - pos).normalized;
        if (Vector3.Distance(pos, towards) > 0.5)
        {
            transform.Translate(direction * Time.deltaTime * fSpeed, Space.World);
        }

    }
}

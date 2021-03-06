using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    public GameObject toObject;
    private Vector3 origPoint;
    float distance;
    public float fSpeed;
    public bool stay;
    bool reached = false;
    public bool activate;

    public void Start()
    {
        origPoint = transform.position;

    }

    public void FixedUpdate()
    {
        if (activate) {

            if (!reached) {
                move(transform.position, toObject.transform.position);
            } else {
                if (!stay) {
                    distance = Vector3.Distance(transform.position, origPoint);
                    if (distance > .1) {
                        move(transform.position, origPoint);
                    } else {
                        reached = false;
                    }
                }
            }
        }
    }

    void move(Vector3 pos, Vector3 towards)
    {
        Vector3 direction = (towards - pos).normalized;
        transform.Translate(direction * Time.deltaTime * fSpeed);
        float distanceleft = Vector3.Distance(transform.position, toObject.transform.position);

        
        if (distanceleft <= 1)
        {
            reached = true;
        }

    }


}

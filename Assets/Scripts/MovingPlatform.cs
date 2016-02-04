using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    /* Public Variables */
    public Transform movingPlatform;
    public Transform start;
    public Transform end;
    public Vector3 newPosition;
    public float smooth;
    public float resetTime;
    /* Private Variables */
    public string currentState;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        movingPlatform.position = Vector3.Lerp(movingPlatform.position, newPosition, smooth * Time.deltaTime);
    }

    void ChangeTarget()
    {
        if (currentState == "Moving to start")
        {
            currentState = "Moving to end";
            newPosition = end.position;
        }
        else if (currentState == "Moving to end")
        {
            currentState = "Moving to start";
            newPosition = start.position;
        }
        else if (currentState == "")
        {
            currentState = "Moving to end";
            newPosition = end.position;
        }

        Invoke("ChangeTarget", resetTime);
    }
}

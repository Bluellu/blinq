using UnityEngine;
using System.Collections;

/* Allows player to turn a moving platform on. */
public class TriggerSimpleActivation : MonoBehaviour {

    public MovingPlatform platform;
    public bool stoppable;  //Indicates this platform stops moving when player leaves trigger.


    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            platform.activate = true;
        }
    }
    void OnTriggerExit(Collider col)
    {
        if ((col.gameObject.tag == "Player") && stoppable)
        {
            platform.activate = false;
        }
    }
}

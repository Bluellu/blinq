using UnityEngine;
using System.Collections;

public class ExtendPlatform : MonoBehaviour {

    public Transform moveTo;
    public GameObject extension;
    public float smooth;
    void OnTriggerEnter(Collider col)
    {
        Debug.Log("Collided");
        if (col.gameObject.name == "Player")
        {
            Debug.Log("With player");
            Debug.Log(extension.transform.position);
            Debug.Log(moveTo.position);
            extension.transform.position = moveTo.position;
        }
            
    }
}

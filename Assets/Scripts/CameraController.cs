using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject PlayerChar;
    public float cameraXval = 0;
    public float cameraYval = 3;
    public float cameraZval = -5;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosit = PlayerChar.transform.position;
        float cameraX = PlayerChar.transform.position.x + cameraXval;
        float cameraY = PlayerChar.transform.position.y + cameraYval;
        float cameraZ = PlayerChar.transform.position.z + cameraZval;
        transform.position = new Vector3(cameraX, cameraY, cameraZ);
        transform.LookAt(playerPosit);


    }
}

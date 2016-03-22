using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{

    public GameObject PlayerChar;
    public GameObject EndObj;
    public bool lastScene;
    private LampStagController LSC;
    private LampController LC;
    public float cameraXval = 0;
    public float cameraYval = 3;
    public float cameraZval = -5;

    private float speed;
    private float chaseRange;
    private float range;

    // Use this for initialization
    void Start()
    {
        speed = 4.0f;
        chaseRange = 10.0f;
        if(lastScene)
            LSC = EndObj.GetComponent<LampStagController>();
        else
            LC = EndObj.GetComponent<LampController>();

    }

    // Update is called once per frame
    void Update()
    {
        if (lastScene)
        {
            if (!LSC.ReachedEnd)
            {
                Vector3 cameraPosit = transform.position;

                float cameraX = ((PlayerChar.transform.position.x + cameraXval) * .05F) + (transform.position.x * .95F);
                float cameraY = ((PlayerChar.transform.position.y + cameraYval) * .05F) + (transform.position.y * .95F);
                float cameraZ = ((PlayerChar.transform.position.z + cameraZval) * .05F) + (transform.position.z * .95F);

                transform.position = new Vector3(cameraX, cameraY, cameraZ);
                cameraPosit = new Vector3(cameraX - cameraXval, cameraY - cameraYval, cameraZ - cameraZval);
                transform.LookAt(cameraPosit);
            }
            else
            {
                transform.Translate(Vector3.forward * 8 * Time.deltaTime);

            }
        }
        else
        {
            if (!LC.ReachedEnd)
            {
                Vector3 cameraPosit = transform.position;

                float cameraX = ((PlayerChar.transform.position.x + cameraXval) * .05F) + (transform.position.x * .95F);
                float cameraY = ((PlayerChar.transform.position.y + cameraYval) * .05F) + (transform.position.y * .95F);
                float cameraZ = ((PlayerChar.transform.position.z + cameraZval) * .05F) + (transform.position.z * .95F);

                transform.position = new Vector3(cameraX, cameraY, cameraZ);
                cameraPosit = new Vector3(cameraX - cameraXval, cameraY - cameraYval, cameraZ - cameraZval);
                transform.LookAt(cameraPosit);
            }
            else
            {
                transform.Translate(Vector3.forward * 18 * Time.deltaTime);

            }
        }



    }

}

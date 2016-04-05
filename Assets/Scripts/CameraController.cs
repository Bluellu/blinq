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

    private bool BeginningAnim;
    private bool toGoal;
    private float lerpingValue;

    private Vector3 StartCameraPosit, EndCameraPosit;
    private Vector3 StartCameraLook, EndCameraLook, CameraLook;

    // Use this for initialization
    void Start()
    {
        BeginningAnim = true;
        toGoal = true;
        lerpingValue = 1.3f;
        speed = 4.0f;
        chaseRange = 10.0f;
        if(lastScene)
            LSC = EndObj.GetComponent<LampStagController>();
        else
            LC = EndObj.GetComponent<LampController>();


        float StartCameraX = (PlayerChar.transform.position.x + cameraXval);
        float StartCameraY = (PlayerChar.transform.position.y + cameraYval);
        float StartCameraZ = (PlayerChar.transform.position.z + cameraZval);

        StartCameraPosit = new Vector3(StartCameraX, StartCameraY, StartCameraZ);
        StartCameraLook = new Vector3(StartCameraX - cameraXval, StartCameraY - cameraYval, StartCameraZ - cameraZval);

        float EndCameraX = (EndObj.transform.position.x + cameraXval);
        float EndCameraY = (EndObj.transform.position.y + cameraYval);
        float EndCameraZ = (EndObj.transform.position.z + cameraZval);

        EndCameraPosit = new Vector3(EndCameraX, EndCameraY, EndCameraZ);
        transform.position = StartCameraPosit;
        transform.LookAt(StartCameraLook);

    }

    // Update is called once per frame
    void Update()
    {
        if(!BeginningAnim)
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
        else
            {
                PlayerChar.GetComponent<PlayerController>().enabled = false;
                transform.position = Vector3.Lerp(StartCameraPosit, EndCameraPosit, Mathf.SmoothStep(0.0f, 1.0f, lerpingValue));
                lerpingValue -= Time.deltaTime * 0.3f;
                if (lerpingValue <= 0)
                {
                    PlayerChar.GetComponent<PlayerController>().enabled = true;
                    BeginningAnim = false;
                }
            }        
    }

}

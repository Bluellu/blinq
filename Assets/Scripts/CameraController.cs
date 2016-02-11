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
		
	//Vector3 playerPosit = PlayerChar.transform.position;
	//cameraPosit = new Vector3(transform.position.x - cameraXval, transform.position.y - cameraYval, transform.position.z - cameraZval);

		//if (Vector3.Distance(playerPosit,cameraPosit) > .5F)
		//{
		
		Vector3 cameraPosit = transform.position;
        
		float cameraX = ((PlayerChar.transform.position.x + cameraXval)*.05F)+(transform.position.x*.95F);
		float cameraY = ((PlayerChar.transform.position.y + cameraYval)*.05F)+(transform.position.y*.95F);
		float cameraZ = ((PlayerChar.transform.position.z + cameraZval)*.05F)+(transform.position.z*.95F);

        transform.position = new Vector3(cameraX, cameraY, cameraZ);
		cameraPosit = new Vector3(cameraX - cameraXval, cameraY - cameraYval, cameraZ - cameraZval);
		transform.LookAt(cameraPosit);

		//}

	}

}

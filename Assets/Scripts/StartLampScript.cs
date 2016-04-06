using UnityEngine;
using System.Collections;

public class StartLampScript : MonoBehaviour {

    private GameObject MainCameraObj, PlayerChar;
    public GameObject Particles;

    // Use this for initialization
    void Start () {
        MainCameraObj = GameObject.FindGameObjectWithTag("MainCamera");
        PlayerChar = GameObject.FindGameObjectWithTag("Player");
        RenderPlayerModel(false);
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        


    }
	
	// Update is called once per frame
	void Update () {

        
        if (!CameraController.BeginningAnim)
        {
            PlayerChar.GetComponent<PlayerController>().enabled = false;
            StartCoroutine(Example());
            
        }
	}


    public void RenderPlayerModel(bool bSwitch)
    {
        SkinnedMeshRenderer[] modelRen  = PlayerChar.GetComponentsInChildren<SkinnedMeshRenderer>();
        foreach (SkinnedMeshRenderer ren in modelRen)
        {
            ren.enabled = bSwitch;
        }

    }

    IEnumerator Example()
    {

        yield return new WaitForSeconds(1);
        Instantiate(Particles, transform.position, new Quaternion(0, 0, 0, 90));
        RenderPlayerModel(true);
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        PlayerChar.GetComponent<PlayerController>().enabled = true;
        
        Destroy(gameObject);

    }
}

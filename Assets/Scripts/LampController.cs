using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LampController : MonoBehaviour {

    public int SceneToTransitionTo;
    //public GameObject Stag;
    public GameObject Lamp;
    public GameObject Particles;
    public bool ReachedEnd;
    //private SkinnedMeshRenderer StagSMR;
    private MeshRenderer LampMR;

    // Use this for initialization
    void Start()
    {
        //StagSMR = Stag.GetComponentInChildren<SkinnedMeshRenderer>();
        //StagSMR.enabled = false;
        LampMR = Lamp.GetComponent<MeshRenderer>();
        LampMR.enabled = true;
        ReachedEnd = false;
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Instantiate(Particles, transform.position, new Quaternion(0, 0, 0, 90));
            LampMR.enabled = false;
            //StagSMR.enabled = true;
            ReachedEnd = true;
            StartCoroutine(Example());



        }


    }
    IEnumerator Example()
    {
        print(Time.time);
        yield return new WaitForSeconds(10);
        SceneManager.LoadScene(SceneToTransitionTo);

        print(Time.time);
    }
}

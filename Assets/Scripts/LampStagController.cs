using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LampStagController : MonoBehaviour
{
    public int SceneToTransitionTo;
    public GameObject Stag;
    public GameObject Lamp;
    public GameObject Particles;
    public bool ReachedEnd;
    private SkinnedMeshRenderer StagSMR;
    private MeshRenderer LampMR;

    //Background music (attached to camera).
    public AudioSource backgroundMusic;
    public Text thanksText;

    // Use this for initialization
    void Start()
    {
        //Make thank you text invisible.
        thanksText.color = new Color(0, 0, 1, 0.0f);

        StagSMR = Stag.GetComponentInChildren<SkinnedMeshRenderer>();
        StagSMR.enabled = false;
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
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            playerObject.GetComponent<PlayerController>().enabled = false;

            Instantiate(Particles, transform.position, new Quaternion(0, 0, 0, 90));
            LampMR.enabled = false;
            StagSMR.enabled = true;
            ReachedEnd = true;
            StartCoroutine(Example());
            


        }

        
    }
    IEnumerator Example()
    {
        print(Time.time);

        //Stop background music and play victory song.
        backgroundMusic.Stop();
        GetComponent<AudioSource>().Play();
        StartCoroutine("thankYou");

        yield return new WaitForSeconds(18);
        SceneManager.LoadScene(SceneToTransitionTo);

        print(Time.time);
    }

    //Thank you text
    IEnumerator thankYou() {
        yield return new WaitForSeconds(7);
        thanksText.color = new Color(0, 0.5f, 0.5f, 1.0f);
        yield return new WaitForSeconds(8);

        thanksText.CrossFadeAlpha(0.0f, 2.5f, false);
    }
    //
}

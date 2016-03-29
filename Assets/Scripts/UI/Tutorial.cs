using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour {
    public Text tutorialText;
    Color startColour;
    Color endColour;

    // Use this for initialization
    void Start () {
       tutorialText.color = new Color(0, 0, 1, 0.0f);
    }
	
	// Update is called once per frame
	void OnTriggerEnter (Collider coll) {
        if (coll.gameObject.tag == "Player")
        {
            StartCoroutine("fade");
        }       	
	}

    IEnumerator fade()    {
        tutorialText.color = new Color(1f, 1f, 1f, 1.0f);
        yield return new WaitForSeconds(6);
        //Color.Lerp(tutorialText.color, Color.clear, 10 * Time.deltaTime);
        tutorialText.CrossFadeAlpha(0.0f, 2.5f, false);
        //Destroy(tutorialText);
    }
}
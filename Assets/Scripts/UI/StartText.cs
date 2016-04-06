using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartText : MonoBehaviour {
    public Text tutorialText;

    // Use this for initialization
    void Start () {
        tutorialText.color = new Color(1, 1, 1, 0.0f);
        StartCoroutine("wait");
        StartCoroutine("fade");
    }


    IEnumerator fade()
    {
        tutorialText.color = new Color(3f, 1f, 1f, 1.0f);
        yield return new WaitForSeconds(3);
        //Color.Lerp(tutorialText.color, Color.clear, 10 * Time.deltaTime);
        GetComponent<Text>().CrossFadeAlpha(0.0f, 2.5f, false);
        //Destroy(tutorialText);
    }

    IEnumerator wait() {
        yield return new WaitForSeconds(4);    
    }

}

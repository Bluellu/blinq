using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartText : MonoBehaviour {

    // Use this for initialization
    void Start () {
        StartCoroutine("fade");
    }


    IEnumerator fade()
    {

        yield return new WaitForSeconds(4);
        //Color.Lerp(tutorialText.color, Color.clear, 10 * Time.deltaTime);
        GetComponent<Text>().CrossFadeAlpha(0.0f, 2.5f, false);
        //Destroy(tutorialText);
    }

}

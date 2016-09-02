using UnityEngine;
using System.Collections;

public class FadeLight : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		var lgt = GetComponent<Light>();
		lgt.intensity = lgt.intensity-(3.0F*Time.deltaTime);
		lgt.range = lgt.range-(6.0F*Time.deltaTime);

		transform.Translate(Vector3.up * 5.0F * Time.deltaTime);
	}
}

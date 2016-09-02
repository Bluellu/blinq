using UnityEngine;
using System.Collections;

public class CeaseWindZone : MonoBehaviour
{

	// Use this for initialization
	void Start ()
	{
		var wind = GetComponent<WindZone>();
		Destroy(wind, 0.5F);
	}

	// Update is called once per frame
	void Update ()
	{

	}
}


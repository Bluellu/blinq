using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

    private Animator anim;
    private float vert, horiz;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {
        vert = Input.GetAxis("Vertical");
        horiz = Input.GetAxis("Horizontal");

        if(Mathf.Abs(vert) != 0 || Mathf.Abs(horiz) !=  0)
            anim.SetFloat("walk", 1);
        else
            anim.SetFloat("walk", 0);



    }
}

using UnityEngine;
using System.Collections;

public class AnimationController : MonoBehaviour {

    private Animator anim;
	private float vert, horiz; 
	private int jumpLength;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	
	}
	
	// Update is called once per frame
	void Update () {
        vert = Input.GetAxis("Vertical");
        horiz = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown("Jump"))
        {
			anim.SetBool("jump", true);
			jumpLength = 30;
        }
		else if (jumpLength > 0)
		{
			jumpLength -= 1;

			if (jumpLength == 0)
			{
            	anim.SetBool("jump", false);
			}
        }


        if (vert == 0.0f && horiz == 0.0f)
        {
            anim.SetFloat("walk", 0.0f);
        }
        else if (vert > 0.7f || vert < -0.7f || horiz > 0.7f || horiz < -0.7f)
        {
            anim.SetFloat("walk", 0.7f);
        }
        else if ((vert > 0.0f && vert < 0.7f) || (vert < 0f && vert > -0.7f) || (horiz > 0.0f && horiz < 0.7f) || (horiz < 0.0f && horiz > -0.7f))
        {
            anim.SetFloat("walk", 0.3f);
        }
        
        
        
        


        //if(Mathf.Abs(vert) != 0 || Mathf.Abs(horiz) !=  0)
        //    anim.SetFloat("walk", 1);
        //else
        //    anim.SetFloat("walk", -1);



    }
}

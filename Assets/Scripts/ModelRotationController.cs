using UnityEngine;
using System.Collections;

public class ModelRotationController : MonoBehaviour {

    private Vector3 moveDirection = Vector3.zero;
	public PlayerController playerController;
    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
		if (playerController.isGrounded)
		{
			moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
		}
		else
		{
			moveDirection = new Vector3(playerController.moveDirection.x, 0, playerController.moveDirection.z);	
		}

		if (moveDirection != Vector3.zero)
        {
            Quaternion LookRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, LookRotation, Time.deltaTime*10);
		}
    }
}

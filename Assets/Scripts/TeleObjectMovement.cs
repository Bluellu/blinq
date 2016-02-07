using UnityEngine;
using System.Collections;

public class TeleObjectMovement : MonoBehaviour {
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float rotateSpeed = 1.0F;
    private Vector3 moveDirection = Vector3.zero;
    // Use this for initialization
    void Start () {
	
	}

    void FixedUpdate()
    {
        CharacterController controller = GetComponent<CharacterController>();


        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateSpeed, 0);
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));

            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

    }
}

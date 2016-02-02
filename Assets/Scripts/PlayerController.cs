using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float rotateSpeed = 1.0F;
    private Vector3 moveDirection = Vector3.zero;

    private TeleportationController teleportationController;
    /*
    void FixedUpdate()
    {
        CharacterController controller = GetComponent<CharacterController>();
        
        teleportationController = gameObject.GetComponent<TeleportationController>();
        if (Input.GetAxis("Vertical") > 0)
        {
            
            //teleportationController.nState = 3;
        }
        
        

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
    */

    void Update()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            
            

            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;

        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);
    }
}

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject Marker;
    public float speed = 6.0F;
    public float jumpSpeed = 8.0F;
    public float gravity = 20.0F;
    public float rotateSpeed = 1.0F;
    private Vector3 moveDirection = Vector3.zero;
    public bool canPlayerControl;

    private bool bDisplayEnd;

    private TeleportationController teleportationController;
    void Start()
    {
        bDisplayEnd = false;
        canPlayerControl = true;
        teleportationController = Marker.GetComponent<TeleportationController>();
    }

    void FixedUpdate()
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
        if (canPlayerControl)
        {
            controller.Move(moveDirection * Time.deltaTime);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "ActivationBoundary")
        {
            Marker.transform.parent = gameObject.transform;
            teleportationController.setRadius(new Vector3(transform.position.x, transform.position.y, transform.position.z));
            teleportationController.canTeleport = true;
            Destroy(other.gameObject);
        }

        if (other.tag == "Floor")
        {
            //Need to implement restart condition if player hits bounding wall
            //transform.position = new Vector3(-45f, 4.7f, -8f);
            SceneManager.LoadScene(0);


        }

        if (other.tag == "LevelEnd")
        {
            Time.timeScale = 0;
            bDisplayEnd = true;
        }

    }

    void OnGUI()
    {
        if (bDisplayEnd)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2 + 10, 100, 50), "Restart"))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene(0);
            }
        }
    }
}

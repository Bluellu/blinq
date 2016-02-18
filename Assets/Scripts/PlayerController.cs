using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject Marker;
    public float speed = 8.0F;
    public float jumpSpeed = 6.0F;
    public float gravity = 15.0F;
    public float rotateSpeed = 3.0F;
    public Vector3 moveDirection = Vector3.zero;
    public bool canPlayerControl;
	public bool isGrounded;

    private bool bDisplayEnd;

    private TeleportationController teleportationController;
    private MandalaMovementController mandalaMovementController;
    void Start()
    {
        bDisplayEnd = false;
        canPlayerControl = true;
        teleportationController = Marker.GetComponent<TeleportationController>();
        mandalaMovementController = Marker.GetComponent<MandalaMovementController>();
    }

    void FixedUpdate()
    {
        CharacterController controller = GetComponent<CharacterController>();
        if (controller.isGrounded)
        {
			isGrounded = true;
            moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection *= speed;
            if (Input.GetButton("Jump"))
                moveDirection.y = jumpSpeed;
        }
		else
		{	
			isGrounded = false;
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
            Marker.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            teleportationController.canTeleport = true;
            mandalaMovementController.canTeleport = true;
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

        if (other.tag == "NextLevel")
        {
            SceneManager.LoadScene(1);
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

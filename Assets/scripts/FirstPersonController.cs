using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class FirstPersonController : MonoBehaviour {

	public LayerMask groundMask;
	
	[Range(45f, 70f)]
	public float fieldOfView;

	// Movement //
	[Range(4.0f, 8.0f)]
	public float speed;

	// [Range(10f, 100f)]
	// public float jumpForce;

	// private bool canJump = true;
	// private bool isGrounded;

	// Camera //
	[Range(3.0f, 6.0f)]
	public float mouseSensitivityX;
	[Range(3.0f, 6.0f)]
	public float mouseSensitivityY;

	// the amount of rotation that should be added to the camera
	private float verticalLookRotation;

	// Members
	private Rigidbody body;

	// Methods //

	void Awake() {

		body = GetComponent<Rigidbody>();

	}

	// Use this for initialization
	void Start () {
		
		
		
	}
	
	// Update is called once per frame
	void Update () {
		
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");

		// Move the body
		this.transform.Translate(new Vector3(inputX / speed, this.transform.position.y, inputY / speed));

		// First person rotations //
		// This rotates the player by the up vector (y)
		transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
		
		// 
		verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
		verticalLookRotation = Mathf.Clamp(verticalLookRotation, -fieldOfView, fieldOfView);

		Camera.main.transform.localEulerAngles = Vector3.left * verticalLookRotation;
		
		// Should a jump mechanic be added? //
		
		// if (Input.GetButtonDown("Jump") && isGrounded) {
		// 	body.AddForce(transform.up * jumpForce);
		// }

		// Ray ray = new Ray(transform.position, -transform.up);
		// RaycastHit hit;

		// if (Physics.Raycast(ray, out hit, 1.1f, groundMask)) {
		// 	isGrounded = true;
		// }
		// else {
		// 	isGrounded = false;
		// }

	}

}
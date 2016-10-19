using UnityEngine;
using System.Collections;

[RequireComponent (typeof(Rigidbody))]
public class FirstPersonController : MonoBehaviour {

	public LayerMask groundMask;
	
	[Range(30f, 70f)]
	public float fieldOfView = 45f;

	// Movement //
	[Range(4.0f, 8.0f)]
	public float speed = 6.0f;

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

	// Methods //

	void Awake() {

		

	}
	
	// Update is called once per frame
	void Update () {
		
		float inputX = Input.GetAxis("Horizontal");
		float inputY = Input.GetAxis("Vertical");
		
		Vector3 pos = this.transform.position;
		transform.position = new Vector3(
			pos.x + inputX * speed *  Time.deltaTime, 
			pos.y,
			pos.z + inputY * speed *  Time.deltaTime
			);

		// First person rotations //
		// This rotates the player by the up vector (y)
		transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX);
		
		// Calculate how much should the camera rotate vertically
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
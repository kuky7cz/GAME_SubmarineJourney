
using UnityEngine;

namespace SubmarineJourney.Character {
	[RequireComponent(typeof(CharacterController))]
	public class PlayerController : MonoBehaviour {
		[Header("Movement")]
		[SerializeField] private float walkSpeed = 5f;
		[SerializeField] private float mouseSensitivity = 2f;
		[SerializeField] private float jumpForce = 5f;
		[SerializeField] private float gravity = -9.81f;

		private CharacterController controller;
		private Vector3 velocity;
		private float xRotation = 0f;
		private Camera playerCamera;

		private void Start() {
			controller = GetComponent<CharacterController>();
			playerCamera = GetComponentInChildren<Camera>();
			Cursor.lockState = CursorLockMode.Locked;
		}

		private void Update() {
			HandleRotation();
			HandleMovement();
		}

		private void HandleRotation() {
			float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
			float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

			xRotation -= mouseY;
			xRotation = Mathf.Clamp(xRotation, -90f, 90f);

			playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
			transform.Rotate(Vector3.up * mouseX);
		}

		private void HandleMovement() {
			float x = Input.GetAxis("Horizontal");
			float z = Input.GetAxis("Vertical");

			Vector3 move = transform.right * x + transform.forward * z;
			controller.Move(move * walkSpeed * Time.deltaTime);

			if (controller.isGrounded && velocity.y < 0) {
				velocity.y = -2f;
			}

			if (Input.GetButtonDown("Jump") && controller.isGrounded) {
				velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
			}

			velocity.y += gravity * Time.deltaTime;
			controller.Move(velocity * Time.deltaTime);
		}
	}
}

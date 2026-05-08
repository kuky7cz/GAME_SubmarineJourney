
using UnityEngine;
using UnityEngine.InputSystem; // Přidáno pro nový Input System

namespace SubmarineJourney.Character {
	[RequireComponent(typeof(CharacterController))]
	public class PlayerController : MonoBehaviour {
		[Header("Movement")]
		[SerializeField] private float walkSpeed = 5f;
		[SerializeField] private float mouseSensitivity = 0.1f; // Upraveno pro nový Input System (delta)
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
			if (Mouse.current == null) return;

			Vector2 mouseDelta = Mouse.current.delta.ReadValue() * mouseSensitivity;
			float mouseX = mouseDelta.x;
			float mouseY = mouseDelta.y;

			xRotation -= mouseY;
			xRotation = Mathf.Clamp(xRotation, -90f, 90f);

			playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
			transform.Rotate(Vector3.up * mouseX);
		}

		private void HandleMovement() {
			if (Keyboard.current == null) return;

			float x = 0f;
			float z = 0f;

			if (Keyboard.current.aKey.isPressed) x -= 1f;
			if (Keyboard.current.dKey.isPressed) x += 1f;
			if (Keyboard.current.wKey.isPressed) z += 1f;
			if (Keyboard.current.sKey.isPressed) z -= 1f;

			Vector3 move = transform.right * x + transform.forward * z;
			controller.Move(move * walkSpeed * Time.deltaTime);

			if (controller.isGrounded && velocity.y < 0) {
				velocity.y = -2f;
			}

			if (Keyboard.current.spaceKey.wasPressedThisFrame && controller.isGrounded) {
				velocity.y = Mathf.Sqrt(jumpForce * -2f * gravity);
			}

			velocity.y += gravity * Time.deltaTime;
			controller.Move(velocity * Time.deltaTime);
		}
	}
}

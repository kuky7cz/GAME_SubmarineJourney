// Modified by Gemini AI
using UnityEngine;
using UnityEngine.InputSystem; // Přidáno pro nový Input System
using SubmarineJourney.Submarine;
using SubmarineJourney.Character;
using SubmarineJourney.Core;

namespace SubmarineJourney.Character {
	public class InteractionSystem : MonoBehaviour {
		[SerializeField] private float interactionDistance = 3f;
		[SerializeField] private LayerMask interactableLayer;
		
		private Camera playerCamera;
		private string currentInteractText;

		private void Start() {
			playerCamera = GetComponentInChildren<Camera>();
		}

		private void Update() {
			UpdateInteraction();
			if (Keyboard.current != null && Keyboard.current.eKey.wasPressedThisFrame) {
				TryInteract();
			}
		}

		private void UpdateInteraction() {
			Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
			if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, interactableLayer)) {
				if (hit.collider.TryGetComponent(out IInteractable interactable)) {
					currentInteractText = interactable.GetInteractText();
					return;
				}
			}
			currentInteractText = "";
		}

		private void TryInteract() {
			Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
			if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance, interactableLayer)) {
				if (hit.collider.TryGetComponent(out IInteractable interactable)) {
					interactable.Interact();
				}
			}
		}

		public string GetCurrentInteractText() {
			return currentInteractText;
		}
	}
}

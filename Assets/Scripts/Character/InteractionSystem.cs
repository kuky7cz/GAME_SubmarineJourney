using UnityEngine;
using SubmarineJourney.Core;
using SubmarineJourney.Core.DI;

namespace SubmarineJourney.Character {
	public class InteractionSystem : BaseMonoBehaviour {
		[SerializeField] private float interactionDistance = 3f;
		[SerializeField] private LayerMask interactableLayer;
		
		private Camera playerCamera;
		private string currentInteractText;

		protected override void Awake() {
			base.Awake();
			ServiceRegistry.Register(this);
		}

		private void OnDestroy() {
			ServiceRegistry.Unregister<InteractionSystem>();
		}

		private void Start() {
			playerCamera = GetComponentInChildren<Camera>();
		}

		private void Update() {
			UpdateInteraction();
			if (Input.GetKeyDown(KeyCode.E)) {
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

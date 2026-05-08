
using UnityEngine;
using UnityEngine.InputSystem; // Přidáno pro nový Input System
using SubmarineJourney.Core;

namespace SubmarineJourney.Systems {
	public class SteeringTerminal : MonoBehaviour, IInteractable {
		[SerializeField] private bool isBeingUsed = false;
		
		private Vector2 movementInput;
		private float verticalInput;

		private void Update() {
			if (isBeingUsed) {
				if (Keyboard.current != null) {
					movementInput.x = (Keyboard.current.aKey.isPressed ? -1 : 0) + (Keyboard.current.dKey.isPressed ? 1 : 0);
					movementInput.y = (Keyboard.current.sKey.isPressed ? -1 : 0) + (Keyboard.current.wKey.isPressed ? 1 : 0);
					verticalInput = (Keyboard.current.spaceKey.isPressed ? 1 : 0) - (Keyboard.current.leftCtrlKey.isPressed ? 1 : 0);

					if (Keyboard.current.escapeKey.wasPressedThisFrame || Keyboard.current.eKey.wasPressedThisFrame) {
						ExitTerminal();
					}
				}
			}
		}

		public void Interact() {
			if (!isBeingUsed) {
				EnterTerminal();
			}
		}

		private void EnterTerminal() {
			isBeingUsed = true;
			// Tady by se mohl vypnout pohyb hráče
			Debug.Log("Ovládáš ponorku...");
		}

		private void ExitTerminal() {
			isBeingUsed = false;
			movementInput = Vector2.zero;
			verticalInput = 0;
			Debug.Log("Opustil jsi terminál.");
		}

		public string GetInteractText() {
			return isBeingUsed ? "" : "Ovládat ponorku";
		}

		public Vector2 GetMovementInput() {
			return movementInput;
		}

		public float GetVerticalInput() {
			return verticalInput;
		}

		public bool IsInUse() {
			return isBeingUsed;
		}
	}
}

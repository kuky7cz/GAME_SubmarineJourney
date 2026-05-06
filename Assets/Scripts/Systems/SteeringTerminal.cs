
using UnityEngine;
using SubmarineJourney.Core;

namespace SubmarineJourney.Systems {
	public class SteeringTerminal : MonoBehaviour, IInteractable {
		[SerializeField] private bool isBeingUsed = false;
		
		private Vector2 movementInput;
		private float verticalInput;

		private void Update() {
			if (isBeingUsed) {
				movementInput.x = Input.GetAxis("Horizontal");
				movementInput.y = Input.GetAxis("Vertical");
				verticalInput = (Input.GetKey(KeyCode.Space) ? 1 : 0) - (Input.GetKey(KeyCode.LeftControl) ? 1 : 0);

				if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.E)) {
					ExitTerminal();
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


using UnityEngine;
using UnityEngine.InputSystem; // Přidáno pro nový Input System
using SubmarineJourney.Submarine;

namespace SubmarineJourney.Character {
	public class Welder : MonoBehaviour {
		[SerializeField] private float repairPower = 10f;
		[SerializeField] private float energyConsumption = 5f;
		[SerializeField] private float range = 2f;
		
		private Camera playerCamera;

		private void Start() {
			playerCamera = GetComponentInParent<Camera>();
		}

		private void Update() {
			if (Mouse.current != null && Mouse.current.leftButton.isPressed) {
				TryRepair();
			}
		}

		private void TryRepair() {
			Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
			if (Physics.Raycast(ray, out RaycastHit hit, range)) {
				if (hit.collider.TryGetComponent(out HullSection aSection)) {
					aSection.Repair(repairPower * Time.deltaTime);
					Debug.Log($"Opravuji sekci: {aSection.name}");
				}
			}
		}
	}
}


using UnityEngine;
using SubmarineJourney.Core;
using SubmarineJourney.Submarine;

namespace SubmarineJourney.Systems {
	public class WaterPump : MonoBehaviour, IInteractable {
		[SerializeField] private HullSection targetSection;
		[SerializeField] private float pumpRate = 0.1f;
		[SerializeField] private bool isActive = false;

		private void Update() {
			if (isActive && targetSection != null) {
				targetSection.DrainWater(pumpRate * Time.deltaTime);
			}
		}

		public void Interact() {
			isActive = !isActive;
			Debug.Log($"Čerpadlo na {gameObject.name} je nyní {(isActive ? "ZAPNUTÉ" : "VYPNUTÉ")}");
		}

		public string GetInteractText() {
			return isActive ? "Vypnout čerpadlo" : "Zapnout čerpadlo";
		}

		public void SetTargetSection(HullSection aSection) {
			targetSection = aSection;
		}
	}
}

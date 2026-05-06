
using UnityEngine;
using SubmarineJourney.Core;

namespace SubmarineJourney.Systems {
	public class Reactor : MonoBehaviour, IInteractable {
		[Header("Reactor Settings")]
		[SerializeField] private float maxPowerOutput = 1000f;
		[SerializeField] private float fuelConsumptionRate = 1f;
		[SerializeField] private float maxFuel = 1000f;
		
		[Header("Current Status")]
		[SerializeField] private float currentFuel;
		[SerializeField] private float currentPowerOutput;
		[SerializeField] private bool isActive = false;

		private void Start() {
			currentFuel = maxFuel;
			PowerGrid grid = Object.FindFirstObjectByType<PowerGrid>();
			if (grid != null) grid.RegisterReactor(this);
		}

		private void Update() {
			if (isActive && currentFuel > 0) {
				ConsumeFuel();
				CalculatePower();
			} else {
				isActive = false;
				currentPowerOutput = 0;
			}
		}

		private void ConsumeFuel() {
			currentFuel -= fuelConsumptionRate * Time.deltaTime;
			if (currentFuel < 0) currentFuel = 0;
		}

		private void CalculatePower() {
			// Zatím jen statický výkon při zapnutí, v budoucnu podle "teploty"
			currentPowerOutput = maxPowerOutput;
		}

		public void AddFuel(float aAmount) {
			currentFuel += aAmount;
			if (currentFuel > maxFuel) currentFuel = maxFuel;
		}

		public void Interact() {
			if (currentFuel > 0) {
				isActive = !isActive;
				Debug.Log($"Reaktor je nyní {(isActive ? "ZAPNUTÝ" : "VYPNUTÝ")}");
			} else {
				Debug.Log("Reaktor nelze zapnout – nemá palivo!");
			}
		}

		public string GetInteractText() {
			if (currentFuel <= 0) return "Bez paliva";
			return isActive ? "Vypnout reaktor" : "Zapnout reaktor";
		}

		public float GetPowerOutput() {
			return currentPowerOutput;
		}
	}
}

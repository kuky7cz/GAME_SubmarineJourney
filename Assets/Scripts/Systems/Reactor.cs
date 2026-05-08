// Gemini: Modified by Gemini AI
using UnityEngine;
using SubmarineJourney.Core;

namespace SubmarineJourney.Systems {
	public class Reactor : MonoBehaviour, IInteractable {
		[Header("Reactor Settings")]
		[SerializeField] private float maxPowerOutput = 1000f;
		[SerializeField] private float fuelConsumptionRate = 1f; // Měrná jednotka na sekundu
		
		[Header("Current Status")]
		[SerializeField] private bool isActive = false;
		[SerializeField] private float currentPowerOutput;

		private void Start() {
			PowerGridService grid = PowerGridService.instance;
			if (grid != null) grid.RegisterReactor(this);
		}

		private void Update() {
			// Reaktor spotřebovává palivo pouze pokud je aktivní a hra není pozastavená
			if (isActive && !GameStateService.instance.isGamePaused) {
				ConsumeFuel();
				CalculatePower();
			} else {
				isActive = false; // Reaktor se automaticky vypne, když dojde palivo nebo je hra pozastavená
				currentPowerOutput = 0;
			}
		}

		private void ConsumeFuel() {
			if (GameStateService.instance != null) {
				float fuelToConsume = fuelConsumptionRate * Time.deltaTime;
				// Gemini: Spotřebováváme palivo z globálního zdroje.
				GameStateService.instance.fuelLevel -= fuelToConsume;
				if (GameStateService.instance.fuelLevel < 0) {
					GameStateService.instance.fuelLevel = 0;
					isActive = false; // Reaktor se vypne, když dojde palivo
					Debug.Log("Palivo došlo!");
				}
			}
		}

		private void CalculatePower() {
			// Gemini: Zatím jen statický výkon při zapnutí, v budoucnu podle "teploty"
			currentPowerOutput = maxPowerOutput;
		}

		public void AddFuel(float aAmount) {
			if (GameStateService.instance != null) {
				GameStateService.instance.fuelLevel += aAmount;
				if (GameStateService.instance.fuelLevel > GameStateService.instance.maxFuel) { // Předpokládáme maxFuel v GameStateService
					GameStateService.instance.fuelLevel = GameStateService.instance.maxFuel;
				}
			}
		}

		public void Interact() {
			if (GameStateService.instance.fuelLevel > 0) {
				isActive = !isActive;
				Debug.Log($"Reaktor je nyní {(isActive ? "ZAPNUTÝ" : "VYPNUTÝ")}");
			} else {
				Debug.Log("Reaktor nelze zapnout – nemá palivo!");
			}
		}

		public string GetInteractText() {
			if (GameStateService.instance.fuelLevel <= 0) return "Bez paliva";
			return isActive ? "Vypnout reaktor" : "Zapnout reaktor";
		}

		public float GetPowerOutput() {
			return currentPowerOutput;
		}
	}
}

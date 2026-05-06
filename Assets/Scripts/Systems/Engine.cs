
using UnityEngine;

namespace SubmarineJourney.Systems {
	public class Engine : MonoBehaviour {
		[Header("Engine Settings")]
		[SerializeField] private float maxThrust = 2000f;
		[SerializeField] private float fuelConsumptionMultiplier = 1f;

		private PowerConsumer power;
		private float currentThrustOutput;

		private void Start() {
			power = GetComponent<PowerConsumer>();
		}

		public float CalculateThrust(float aInput) {
			if (power == null || !power.IsPowered()) {
				currentThrustOutput = 0;
				return 0;
			}

			// Výkon motoru závisí na dostupnosti elektřiny (efficiency)
			currentThrustOutput = aInput * maxThrust * power.GetEfficiency();
			return currentThrustOutput;
		}

		public float GetCurrentThrust() {
			return currentThrustOutput;
		}
	}
}

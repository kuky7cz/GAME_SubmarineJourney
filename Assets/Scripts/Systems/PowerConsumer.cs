
using UnityEngine;

namespace SubmarineJourney.Systems {
	public class PowerConsumer : MonoBehaviour {
		[SerializeField] private float requiredPower = 50f;
		[SerializeField] private float currentEfficiency = 0f;

		private PowerGrid grid;

		private void Start() {
			grid = Object.FindFirstObjectByType<PowerGrid>();
			if (grid != null) grid.RegisterConsumer(this);
		}

		public void SetPowerAvailability(float aRatio) {
			currentEfficiency = aRatio;
		}

		public float GetRequiredPower() {
			return requiredPower;
		}

		public float GetEfficiency() {
			return currentEfficiency;
		}

		public bool IsPowered() {
			return currentEfficiency > 0.1f;
		}
	}
}

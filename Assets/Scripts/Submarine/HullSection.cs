
using UnityEngine;
using SubmarineJourney.Systems;

namespace SubmarineJourney.Submarine {
	public class HullSection : MonoBehaviour {
		[Header("Hull Integrity")]
		[SerializeField] private float maxHealth = 100f;
		[SerializeField] private float currentHealth;
		[SerializeField] private bool isBroken;

		[Header("Water Settings")]
		[SerializeField] private float waterLevel = 0f;
		[SerializeField] private float floodRate = 0.05f;
		[SerializeField] private float flowRate = 0.02f;

		[Header("Atmosphere")]
		[SerializeField] private float oxygenLevel = 1f;
		[SerializeField] private float oxygenConsumptionRate = 0.005f;

		[Header("Neighbors")]
		[SerializeField] private HullSection neighborSection;
		[SerializeField] private Door connectingDoor;

		private void Start() {
			currentHealth = maxHealth;
		}

		private void Update() {
			HandleFlooding();
			HandleWaterFlow();
			HandleOxygenDecay();
		}

		private void HandleFlooding() {
			if (isBroken && waterLevel < 1f) {
				waterLevel += floodRate * Time.deltaTime;
			}
		}

		private void HandleWaterFlow() {
			if (neighborSection == null || connectingDoor == null || !connectingDoor.IsOpen()) return;

			// Voda teče z vyšší hladiny do nižší
			float difference = waterLevel - neighborSection.GetWaterLevel();
			if (difference > 0.01f) {
				float flow = difference * flowRate * Time.deltaTime;
				waterLevel -= flow;
				neighborSection.AddWater(flow);
			}
		}

		private void HandleOxygenDecay() {
			// Přirozený úbytek kyslíku (simulace spotřeby posádkou)
			// V budoucnu může záviset na počtu entit v místnosti
			oxygenLevel -= oxygenConsumptionRate * Time.deltaTime;
			if (oxygenLevel < 0) oxygenLevel = 0;
		}

		public void TakeDamage(float aDamage) {
			currentHealth -= aDamage;
			if (currentHealth <= 0) {
				currentHealth = 0;
				isBroken = true;
			}
		}

		public void Repair(float aAmount) {
			currentHealth += aAmount;
			if (currentHealth > 0) isBroken = false;
			if (currentHealth > maxHealth) currentHealth = maxHealth;
		}

		public void DrainWater(float aAmount) {
			waterLevel -= aAmount;
			if (waterLevel < 0) waterLevel = 0;
		}

		public void AddWater(float aAmount) {
			waterLevel += aAmount;
			if (waterLevel > 1f) waterLevel = 1f;
		}

		public void AddOxygen(float aAmount) {
			oxygenLevel += aAmount;
			if (oxygenLevel > 1f) oxygenLevel = 1f;
		}

		public float GetWaterLevel() {
			return waterLevel;
		}

		public float GetOxygenLevel() {
			return oxygenLevel;
		}
	}
}

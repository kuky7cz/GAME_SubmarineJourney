
using UnityEngine;
using SubmarineJourney.Submarine;

namespace SubmarineJourney.Systems {
	public class OxygenGenerator : MonoBehaviour {
		[SerializeField] private HullSection targetSection;
		[SerializeField] private float productionRate = 0.02f;
		
		private PowerConsumer power;

		private void Start() {
			power = GetComponent<PowerConsumer>();
		}

		private void Update() {
			if (targetSection != null && power != null && power.IsPowered()) {
				ProduceOxygen();
			}
		}

		private void ProduceOxygen() {
			// Přidává kyslík přímo do sekce
			targetSection.AddOxygen(productionRate * Time.deltaTime);
		}
	}
}

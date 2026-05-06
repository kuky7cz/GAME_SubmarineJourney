
using UnityEngine;
using System.Collections.Generic;

namespace SubmarineJourney.Systems {
	public class PowerGrid : MonoBehaviour {
		[Header("Grid Status")]
		[SerializeField] private float totalProduction;
		[SerializeField] private float totalDemand;
		[SerializeField] private float powerRatio = 1f; // 1 = vĹˇe ok, < 1 = blackout/omezenĂ­

		private List<Reactor> reactors = new List<Reactor>();
		private List<PowerConsumer> consumers = new List<PowerConsumer>();

		private void Update() {
			CalculateGrid();
		}

		private void CalculateGrid() {
			totalProduction = 0;
			totalDemand = 0;

			foreach (var reactor in reactors) {
				totalProduction += reactor.GetPowerOutput();
			}

			foreach (var consumer in consumers) {
				totalDemand += consumer.GetRequiredPower();
			}

			powerRatio = (totalDemand > 0) ? Mathf.Min(1f, totalProduction / totalDemand) : 1f;

			foreach (var consumer in consumers) {
				consumer.SetPowerAvailability(powerRatio);
			}
		}

		public void RegisterReactor(Reactor aReactor) {
			if (!reactors.Contains(aReactor)) reactors.Add(aReactor);
		}

		public void RegisterConsumer(PowerConsumer aConsumer) {
			if (!consumers.Contains(aConsumer)) consumers.Add(aConsumer);
		}
	}
}

using UnityEngine;
using System.Collections.Generic;
using SubmarineJourney.Core;
using SubmarineJourney.Core.DI;

namespace SubmarineJourney.Systems {
	public class PowerGrid : BaseMonoBehaviour {
		[Header("Grid Status")]
		[SerializeField] private float totalProduction;
		[SerializeField] private float totalDemand;
		[SerializeField] private float powerRatio = 1f; // 1 = vše ok, < 1 = blackout/omezení

		private List<Reactor> reactors = new List<Reactor>();
		private List<PowerConsumer> consumers = new List<PowerConsumer>();

		protected override void Awake() {
			base.Awake();
			ServiceRegistry.Register(this);
		}

		private void OnDestroy() {
			ServiceRegistry.Unregister<PowerGrid>();
		}

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

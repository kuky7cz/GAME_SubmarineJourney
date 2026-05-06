using UnityEngine;
using TMPro;
using SubmarineJourney.Character;
using SubmarineJourney.Submarine;
using SubmarineJourney.Systems;
using SubmarineJourney.Core.DI;

namespace SubmarineJourney.Core {
	public class HUDManager : BaseMonoBehaviour {
		[Header("UI References")]
		[SerializeField] private TextMeshProUGUI interactText;
		[SerializeField] private TextMeshProUGUI oxygenText;
		[SerializeField] private TextMeshProUGUI healthText;

		[Inject] private InteractionSystem playerInteraction;
		[Inject] private CharacterHealth playerHealth;

		protected override void Awake() {
			base.Awake();
			ServiceRegistry.Register(this);
		}

		private void OnDestroy() {
			ServiceRegistry.Unregister<HUDManager>();
		}

		private void Update() {
			if (playerInteraction != null && interactText != null) {
				interactText.text = playerInteraction.GetCurrentInteractText();
			}

			if (playerHealth != null) {
				if (oxygenText != null) {
					float o2 = playerHealth.GetCurrentOxygen();
					oxygenText.text = $"O2: {(o2 * 100f):F0}%";
					oxygenText.color = o2 < 0.2f ? Color.red : Color.white;
				}

				if (healthText != null) {
					healthText.text = $"Health: {playerHealth.GetCurrentHealth():F0}";
				}
			}
		}
	}
}

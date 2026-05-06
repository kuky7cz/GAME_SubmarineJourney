
using UnityEngine;
using TMPro;
using SubmarineJourney.Character;
using SubmarineJourney.Submarine;

namespace SubmarineJourney.Core {
	public class HUDManager : MonoBehaviour {
		[Header("UI References")]
		[SerializeField] private TextMeshProUGUI interactText;
		[SerializeField] private TextMeshProUGUI oxygenText;
		[SerializeField] private TextMeshProUGUI healthText;

		private InteractionSystem playerInteraction;
		private CharacterHealth playerHealth;

		private void Start() {
			playerInteraction = Object.FindFirstObjectByType<InteractionSystem>();
			playerHealth = Object.FindFirstObjectByType<CharacterHealth>();
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

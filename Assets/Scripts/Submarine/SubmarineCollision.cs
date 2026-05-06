
using UnityEngine;
using SubmarineJourney.Submarine;

namespace SubmarineJourney.Submarine {
	public class SubmarineCollision : MonoBehaviour {
		[SerializeField] private float damageThreshold = 5f;
		[SerializeField] private float damageMultiplier = 2f;
		
		private HullSection[] sections;

		private void Start() {
			sections = GetComponentsInChildren<HullSection>();
		}

		private void OnCollisionEnter(Collision aCollision) {
			float force = aCollision.relativeVelocity.magnitude;
			
			if (force > damageThreshold) {
				ApplyCollisionDamage(force * damageMultiplier);
			}
		}

		private void ApplyCollisionDamage(float aDamage) {
			if (sections.Length == 0) return;

			// Náhodně vybere sekci, která schytá náraz
			int randomIndex = Random.Range(0, sections.Length);
			sections[randomIndex].TakeDamage(aDamage);
			
			Debug.Log($"Náraz! Sekce {sections[randomIndex].name} poškozena silou {aDamage:F1}");
		}
	}
}

// Gemini: Modified by Gemini AI
using UnityEngine;
using SubmarineJourney.Submarine;
using SubmarineJourney.Core;

namespace SubmarineJourney.Systems {
	public class SeaCreature : MonoBehaviour {
		[SerializeField] private float damage = 10f;
		[SerializeField] private float attackInterval = 3f;
		[SerializeField] private float speed = 5f;
		
		private HullSection targetSection;
		private float nextAttackTime;

		private void Start() {
			// Gemini: Pro HullSection zatím necháme Find, než vytvoříme SubmarineManager
			targetSection = Object.FindFirstObjectByType<HullSection>();
		}

		private void Update() {
			if (targetSection == null) return;

			MoveToTarget();
			TryAttack();
		}

		private void MoveToTarget() {
			Vector3 direction = (targetSection.transform.position - transform.position).normalized;
			transform.position += direction * speed * Time.deltaTime;
		}

		private void TryAttack() {
			if (Time.time >= nextAttackTime && Vector3.Distance(transform.position, targetSection.transform.position) < 2f) {
				targetSection.TakeDamage(damage);
				nextAttackTime = Time.time + attackInterval;
				Debug.Log("Potvora zaútočila na ponorku!");
			}
		}
	}
}

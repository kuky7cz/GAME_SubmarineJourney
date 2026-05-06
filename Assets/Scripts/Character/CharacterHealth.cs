// Gemini: Modified by Gemini AI
using UnityEngine;
using SubmarineJourney.Submarine;

namespace SubmarineJourney.Character {
	public class CharacterHealth : MonoBehaviour {
		public static CharacterHealth instance { get; private set; }

		[SerializeField] private float maxHealth = 100f;
		[SerializeField] private float currentHealth;
		[SerializeField] private float suffocationDamage = 5f;
		[SerializeField] private LayerMask hullLayer;
		
		private HullSection currentSection;

		private void Awake() {
			if (instance == null) {
				instance = this;
			} else {
				Destroy(gameObject);
			}
		}

		private void Start() {
			currentHealth = maxHealth;
		}

		private void Update() {
			UpdateCurrentSection();
			HandleSuffocation();
		}

		private void UpdateCurrentSection() {
			if (Physics.Raycast(transform.position + Vector3.up, Vector3.down, out RaycastHit hit, 2f, hullLayer)) {
				if (hit.collider.TryGetComponent(out HullSection section)) {
					currentSection = section;
				}
			} else {
				currentSection = null;
			}
		}

		private void HandleSuffocation() {
			if (currentSection != null) {
				if (currentSection.GetOxygenLevel() < 0.2f) {
					TakeDamage(suffocationDamage * Time.deltaTime);
				}
			} else {
				TakeDamage(suffocationDamage * 2f * Time.deltaTime);
			}
		}

		public void TakeDamage(float aDamage) {
			currentHealth -= aDamage;
			if (currentHealth <= 0) {
				currentHealth = 0;
				Die();
			}
		}

		private void Die() {
			Debug.Log("Hráč zemřel!");
		}

		public float GetCurrentOxygen() {
			return currentSection != null ? currentSection.GetOxygenLevel() : 0f;
		}

		public float GetCurrentHealth() {
			return currentHealth;
		}
	}
}

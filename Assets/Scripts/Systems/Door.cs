
using UnityEngine;
using SubmarineJourney.Core;

namespace SubmarineJourney.Systems {
	public class Door : MonoBehaviour, IInteractable {
		[SerializeField] private bool isOpen = false;
		[SerializeField] private float openSpeed = 5f;
		
		private void Update() {
			// Zde by byla logika animace nebo pohybu transformu dveří
		}

		public void Interact() {
			isOpen = !isOpen;
			Debug.Log($"Dveře {gameObject.name} jsou nyní {(isOpen ? "OTEVŘENÉ" : "ZAVŘENÉ")}");
		}

		public string GetInteractText() {
			return isOpen ? "Zavřít" : "Otevřít";
		}

		public bool IsOpen() {
			return isOpen;
		}
	}
}

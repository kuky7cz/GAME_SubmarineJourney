
// Gemini: This file dont created whit AI.
// Gemini: read-on, edit-off, delete-off,

using System;
using UnityEngine;



namespace SubmarineJourney.Core.DI {
	/// <summary>
	/// Centrální bod pro manuální reference scény přiřazené v Editoru.
	/// </summary>
	public class SceneReferencesSevice : MonoBehaviour {
		public static SceneReferencesSevice instance { get; private set; }


		[Header("Submarine")]
		public SubmarineJourney.Prefabs.Submarine submarine;
		public SubmarineJourney.Prefabs.Submarine submarinePrefab;


		[Header("World")]
		public GameObject world;



		//[Header("UI")]


		private void Awake() {
			if (instance == null) { instance = this; } 
			else { Debug.LogError("Duplicite Singleton instance"); }
		}

		private void Start() {
			InitializeGlobalServices();
		}

		/// <summary>
		/// Automaticky vyhledá a spustí metody označené [GlobalInit].
		/// </summary>
		public void InitializeGlobalServices() {
			Debug.Log("[SceneReferences] Running global initialization...");
			var assembly = System.Reflection.Assembly.GetExecutingAssembly();
			foreach (var type in assembly.GetTypes()) {
				if (type.Namespace == null || !type.Namespace.StartsWith("SubmarineJourney")) continue;

				foreach (var method in type.GetMethods(System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.NonPublic)) {
					if (method.IsDefined(typeof(GlobalInitAttribute), false)) {
						try {
							method.Invoke(null, null);
							Debug.Log($"[SceneReferences] Invoked {type.Name}.{method.Name}");
						} catch (Exception e) {
							Debug.LogError($"[SceneReferences] Failed to invoke {type.Name}: {e.Message}");
						}
					}
				}
			}
		}
	}
}

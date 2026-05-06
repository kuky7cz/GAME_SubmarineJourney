using UnityEngine;
using UnityEditor;
using SubmarineJourney.Systems;
using SubmarineJourney.Submarine;
using SubmarineJourney.Character;
using SubmarineJourney.Core;

namespace SubmarineJourney.Editor {
    public class GeminiBridge : EditorWindow {
        [MenuItem("Gemini/Bridge/Apply AI Configuration")]
        public static void ApplyAIConfiguration() {
            Debug.Log("[GeminiBridge] Starting scene wiring...");

            // 1. Setup Core Systems
            SetupSystem<PowerGridService>("Systems/PowerGrid");
            SetupSystem<HUDService>("UI/HUD");
            
            // 2. Setup Player
            SetupPlayer();

            // 3. Setup Submarine Hull
            SetupHullSections();

            Debug.Log("[GeminiBridge] Configuration applied successfully.");
        }

        private static void SetupSystem<T>(string aPath) where T : Component {
            GameObject go = GameObject.Find(aPath);
            if (go == null) {
                // Zkusíme najít jen podle jména, pokud cesta nesedí
                string name = aPath.Substring(aPath.LastIndexOf('/') + 1);
                go = GameObject.Find(name);
            }

            if (go != null) {
                if (!go.TryGetComponent<T>(out _)) {
                    go.AddComponent<T>();
                    Debug.Log($"[GeminiBridge] Added {typeof(T).Name} to {go.name}");
                }
            } else {
                Debug.LogWarning($"[GeminiBridge] Target for {typeof(T).Name} not found at: {aPath}");
            }
        }

        private static void SetupPlayer() {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player == null) player = GameObject.Find("Player");

            if (player != null) {
                EnsureComponent<PlayerController>(player);
                EnsureComponent<CharacterHealth>(player);
                EnsureComponent<InteractionSystem>(player);
                Debug.Log("[GeminiBridge] Player components verified.");
            }
        }

        private static void SetupHullSections() {
            var sections = GameObject.FindObjectsByType<HullSection>(FindObjectsSortMode.None);
            foreach (var section in sections) {
                // Zde můžu automaticky nastavit např. Layer na "Hull" pokud existuje
                // section.gameObject.layer = LayerMask.NameToLayer("Hull");
            }
            Debug.Log($"[GeminiBridge] Verified {sections.Length} hull sections.");
        }

        private static void EnsureComponent<T>(GameObject aTarget) where T : Component {
            if (!aTarget.TryGetComponent<T>(out _)) {
                aTarget.AddComponent<T>();
            }
        }

        [MenuItem("Gemini/Bridge/Open Control Panel")]
        public static void ShowWindow() {
            GetWindow<GeminiBridge>("Gemini Bridge");
        }

        private void OnGUI() {
            GUILayout.Label("Gemini AI Bridge Settings", EditorStyles.boldLabel);
            
            if (GUILayout.Button("Apply AI Configuration")) {
                ApplyAIConfiguration();
            }

            if (GUILayout.Button("Scan for Issues")) {
                ScanForIssues();
            }
        }

        private void ScanForIssues() {
            Debug.Log("[GeminiBridge] Scanning for configuration issues...");
            // Zde můžu přidat validace, např. jestli má reaktor palivo, jestli jsou propojené sekce atd.
        }
    }
}


using UnityEngine;
using SubmarineJourney.Systems;

namespace SubmarineJourney.Submarine {
	[RequireComponent(typeof(Rigidbody))]
	public class SubmarineController : MonoBehaviour {
		[Header("References")]
		[SerializeField] private Engine mainEngine;
		[SerializeField] private SteeringTerminal terminal;
		
		[Header("Movement Settings")]
		[SerializeField] private float rotationTorque = 500f;
		[SerializeField] private float depthForce = 1000f;
		
		private Rigidbody rb;

		private void Start() {
			rb = GetComponent<Rigidbody>();
			rb.useGravity = false;
			rb.linearDamping = 0.5f;
			rb.angularDamping = 1f;
		}

		private void FixedUpdate() {
			if (terminal == null || !terminal.IsInUse()) return;

			Vector2 input = terminal.GetMovementInput();
			float vertical = terminal.GetVerticalInput();

			// Pohyb dopĹ™edu/dozadu pĹ™es motor
			if (mainEngine != null) {
				float thrust = mainEngine.CalculateThrust(input.y);
				rb.AddRelativeForce(Vector3.forward * thrust);
			}
			
			// OtĂˇÄŤenĂ­
			rb.AddRelativeTorque(Vector3.up * input.x * rotationTorque);
			
			// ZmÄ›na hloubky
			rb.AddForce(Vector3.up * vertical * depthForce);
		}
	}
}

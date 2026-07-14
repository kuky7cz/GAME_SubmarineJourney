
//
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditorInternal.VersionControl.ListControl;



public class Turret : MonoBehaviour {

	[Header("values")]
	public bool isEntered = false;
	public float sensitivity = 0.5f;


	[SerializeField] private bool lockCursor = true;

	[Header("go CONNECTION ")]
	public Transform trRotor;
	public Transform trHead;
	public InputActionAsset inputAsset;
	public AudioClip clickSound;

	//
	private InputAction lookAction;
	private InputAction clickAction;
	private InputAction btnEsc;
	private float verticalRotationX = 0f; // Pomocná proměnná pro ukládání vertikálního úhlu
	private AudioSource audioSource;


	void Awake() {
		// Najde akce podle textového názvu (Změňte "Player" a názvy akcí podle vašeho schématu)
		lookAction = inputAsset.FindActionMap("Player").FindAction("Look");
		clickAction = inputAsset.FindActionMap("Player").FindAction("Attack");
		btnEsc = inputAsset.FindActionMap("Player").FindAction("Esc");
		//
		audioSource = GetComponent<AudioSource>();
		// TEMP EDITOR
		OnValidate();
	}

	void Start() {

	}

	void Update() {
		ControllerUpdate();
	}

	public void Enter() {
		//
		isEntered = true;
		//
		clickAction.performed += OnClickPerformed;
		//
		if (lockCursor) {
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
		}
		// Načteme si výchozí vertikální rotaci objektu, aby kamera při startu neodskočila
		verticalRotationX = trHead.localEulerAngles.x;
		// Korekce úhlu z rozsahu 0-360 na -180 až 180
		if (verticalRotationX > 180) verticalRotationX -= 360f;
		
	}

	public void Leave() {
		//
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		//
		clickAction.performed -= OnClickPerformed;
		//
		isEntered = false;
	}

	public void ControllerUpdate() {
		if (!isEntered){ return; }

		Vector2 mouseDelta = lookAction.ReadValue<Vector2>();


		// 2. HORIZONTÁLNÍ ROTACE (Osa X myši otáčí tělesem kolem osy Y)
		if (Mathf.Abs(mouseDelta.x) > 0.001f) {
			float horizontalRotation = mouseDelta.x * sensitivity;
			trRotor.Rotate(Vector3.up * horizontalRotation);
		}

		// 3. VERTIKÁLNÍ ROTACE (Osa Y myši otáčí druhým objektem kolem osy X)
		if ( Mathf.Abs(mouseDelta.y) > 0.001f) {
			// Odečítáme, aby pohyb myši nahoru znamenal pohled nahoru (standardní ovládání)
			verticalRotationX -= mouseDelta.y * sensitivity;

			// Omezení (Clamp) rozsahu rotace, aby se hlava/kamera nepřetočila dozadu
			verticalRotationX = Mathf.Clamp(verticalRotationX, -90f, +90f);

			// Aplikování rotace pouze na osu X druhého objektu
			trHead.localRotation = Quaternion.Euler(verticalRotationX, 0f, 0f);
		}

		// Odemykání kurzoru pomocí klávesy Escape pro snazší testování
		if (Keyboard.current.escapeKey.wasPressedThisFrame) {
			Debug.Log("ESC");
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}

	}

	public void OnClickPerformed(InputAction.CallbackContext context) {
		Debug.Log("Tlačítko myši bylo stisknuto.");
		audioSource.PlayOneShot(clickSound);

	}



	// // TEMP EDITOR
	private bool lastState;

	/// <summary>
	/// EDITOR
	/// </summary>
	private void OnValidate() {
		// Kontrola, zda hráč v editoru skutečně změnil hodnotu isEntered
		if (isEntered != lastState) {
			if (isEntered) {
				Enter();
			} else {
				Leave();
			}

			// Uložíme si nový stav, aby se funkce nespouštěly pořád dokola
			lastState = isEntered;
		}
	}
}

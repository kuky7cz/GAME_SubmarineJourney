using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour {

	public float speed = 20f;

	private Vector3 r1;
	private float time = 0f;

	void Start() {

	}

	void Update() {
		
		r1 = transform.localPosition;
		// transform.forward je ve smìru Z
		r1 += transform.up * speed * Time.deltaTime;
		transform.localPosition = r1;
		
		//transform.Translate(Vector3.forward * 20f * Time.deltaTime, Space.Self);
		// Self Destroi
		if (time > 100f) {
			Destroy(this.gameObject);
		}
		time += speed * Time.deltaTime;
	}
}

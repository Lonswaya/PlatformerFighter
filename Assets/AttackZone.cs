using UnityEngine;
using System.Collections;

public class AttackZone : MonoBehaviour {
	private bool inzone;
	private GameObject focus;
	
	void OnTriggerEnter2D(Collider2D col) {
		if (col.GetComponent<BallController>())  {
			inzone = true;
		}
	}
	void OnTriggerStay2D(Collider2D col) {

		if (col.GetComponent<BallController>()) focus = col.gameObject;
	}
	void OnTriggerExit2D(Collider2D col) {
		if (col.GetComponent<BallController>()) {
			inzone = false;
		}
	}

	void Attack() {
		if (inzone) {
			focus.GetComponent<BallController>().TakeDamage(15, this.transform.position);
		}
	}
}

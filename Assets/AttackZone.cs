using UnityEngine;
using System.Collections;

public class AttackZone : MonoBehaviour {
	private bool inzone;
	private GameObject focus;
	
	void OnTriggerEnter2D(Collider2D col) {
		if (col.GetComponent<BallController>() && !col.isTrigger)  {
			focus = col.gameObject; //needs to be parent so you dont get the triggerzone, but the base gameobject
			inzone = true;
		}
	}
	void OnTriggerStay2D(Collider2D col) {

		if (col.GetComponent<BallController>() && !col.isTrigger) focus = col.gameObject;
	}
	void OnTriggerExit2D(Collider2D col) {
		if (col.gameObject == focus) {
			inzone = false;
		}
	}

	void Attack(float damage) {
		if (inzone && focus.GetComponent<BallController>()) {
			focus.GetComponent<BallController>().TakeDamage(damage + Mathf.Sqrt(Vector2.SqrMagnitude(this.transform.parent.rigidbody2D.velocity - focus.rigidbody2D.velocity)), this.transform.position);
		}
	}
}

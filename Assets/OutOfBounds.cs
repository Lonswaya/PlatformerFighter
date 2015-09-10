using UnityEngine;
using System.Collections;

public class OutOfBounds : MonoBehaviour {
	public Vector2 respawnPoint;

	void OnTriggerEnter2D(Collider2D col) {
		if (col.GetComponent<BallController>()) {
			col.transform.position = respawnPoint;
			col.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
			col.GetComponent<BallController>().deaths++;
			col.GetComponent<BallController>().damage = 0;
		}
	}
}

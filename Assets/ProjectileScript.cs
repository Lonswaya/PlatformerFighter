using UnityEngine;
using System.Collections;

public class ProjectileScript : MonoBehaviour {
	public float damage = 3;
	private float timeSince = 0;
	// Update is called once per frame
	void Update () {

		if ((timeSince += Time.deltaTime) > 5) GameObject.Destroy(this.gameObject);
	}
	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.GetComponent<BallController>()) {
			col.gameObject.GetComponent<BallController>().TakeDamage(damage, this.transform.position);
			Destroy(this.gameObject);
		} else if (col.gameObject.GetComponent<ProjectileScript>()) {
			Destroy(this.gameObject);
		}
	}
}

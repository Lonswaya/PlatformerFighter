using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
	public int playerid  = 0;
	public int deaths;
	public float damage;
	public float damageCooldownTime = .5f;
	public float coolDownAttack;

	private int jumpcounter;
	// Use this for initialization
	void Start () {
		this.tag = "Player";
	}
	bool GetLeftKey(float id) {
		if (id == 0) return Input.GetKey(KeyCode.A);
		if (id == 1) return Input.GetKey(KeyCode.J);
		return false;
	}
	bool GetRightKey(float id) {
		if (id == 0) return Input.GetKey(KeyCode.D);
		if (id == 1) return Input.GetKey(KeyCode.L);
		return false;
	}
	bool GetJumpKey(float id) {
		if (id == 0) return Input.GetKeyDown(KeyCode.W);
		if (id == 1) return Input.GetKeyDown(KeyCode.I);
		return false;
	}
	bool GetDownKey(float id) {
		if (id == 0) return Input.GetKey(KeyCode.S);
		if (id == 1) return Input.GetKey(KeyCode.K);
		return false;
	}
	bool GetAttackKey(float id) {
		if (id == 0) return Input.GetKey(KeyCode.E);
		if (id == 1) return Input.GetKey(KeyCode.O);
		return false;
	}


	// Update is called once per frame
	void Update () {
		if (GetLeftKey(playerid)) {
			this.GetComponent<Rigidbody2D>().AddForce(new Vector2(-10, 0));
		} else if (GetRightKey(playerid)) {
			this.GetComponent<Rigidbody2D>().AddForce(new Vector2(10, 0));
		} else if (GetDownKey(playerid)) {
			this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, -15));
		}
		if (GetJumpKey(playerid) && jumpcounter < 2) {
			if (jumpcounter > 0) jumpcounter++;
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(this.GetComponent<Rigidbody2D>().velocity.x, 0);
			this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 300));
		}
		this.coolDownAttack+= Time.deltaTime;
		if (GetAttackKey(playerid) && coolDownAttack > this.damageCooldownTime) {
			coolDownAttack = 0;
			this.transform.Find("TriggerZone").SendMessage("Attack");
		}
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (!col.gameObject.GetComponent<BallController>()) jumpcounter = 0;
	}
	void OnCollisionExit2D(Collision2D col) { // prevent two jumps if you fell off
		if (!col.gameObject.GetComponent<BallController>() && jumpcounter == 0) { 
			jumpcounter++;
		}
	}

	public void TakeDamage(float f, Vector2 placement) {
		print("Ow that hurt " + damage + " much");
		this.damage += f;
		this.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, damage));
		this.GetComponent<Rigidbody2D>().AddForce (f * damage * ((Vector2)this.transform.position - placement));
		//this.rigidbody2D.AddForce (new Vector2(0, damage));

	}



}

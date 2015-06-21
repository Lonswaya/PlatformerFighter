using UnityEngine;
using System.Collections;

public class BallController : MonoBehaviour {
	public int playerid  = 0;
	public int deaths;
	public float damage;
	public float damageCooldownTime = .5f;
	public float damageCooldownTime2 = .2f;
	public float dealingDamage = 15;
	public float coolDownAttack;
	private float delayTime = 0;
	private bool attacking;
	public float attackDelay = .2f;
	public float coolDownAttack2;
	private float delayTime2 = 0;
	private bool attacking2;
	private float projDirection = 1;
	public float projspeed = 1;
	public float projDamage = 3;
	public float attackDelay2 = .1f;
	public GameObject throwable;

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
	bool GetSeccondaryAttackKey(float id) {
		if (id == 0) return Input.GetKey(KeyCode.Q);
		if (id == 1) return Input.GetKey(KeyCode.U);
		return false;
	}


	// Update is called once per frame
	void Update () {
		if (GetLeftKey(playerid)) {
			projDirection = -1;
			this.rigidbody2D.AddForce(new Vector2(-10, 0));
		} else if (GetRightKey(playerid)) {
			projDirection = 1;
			this.rigidbody2D.AddForce(new Vector2(10, 0));
		} else if (GetDownKey(playerid)) {
			this.rigidbody2D.AddForce(new Vector2(0, -15));
		}
		if (GetJumpKey(playerid) && jumpcounter < 2) {
			if (jumpcounter > 0) jumpcounter++;
			this.rigidbody2D.velocity = new Vector2(this.rigidbody2D.velocity.x, 0);
			this.rigidbody2D.AddForce(new Vector2(0, 300));
		}
		this.coolDownAttack+= Time.deltaTime;

		if (GetAttackKey(playerid) && coolDownAttack > this.damageCooldownTime) {
			coolDownAttack = 0;
			attacking = true;
		} 
		if (attacking) {
			delayTime +=Time.deltaTime;
			if (delayTime >= attackDelay) {
				delayTime = 0;
				attacking = false;
				this.transform.FindChild("TriggerZone").SendMessage("Attack", dealingDamage); //the damage you would deal, and your velocity does some more as well
			}
		} else {
			delayTime = 0;
		}
		this.coolDownAttack2+=Time.deltaTime;
		if (GetSeccondaryAttackKey(playerid) && coolDownAttack2 > this.damageCooldownTime2) {
			coolDownAttack2 = 0;
			attacking2 = true;
		}
		if (attacking2) {
			delayTime2 +=Time.deltaTime;
			if (delayTime2 >= attackDelay2) {
				delayTime2 = 0;
				attacking2 = false;
				GameObject proj = (GameObject)Instantiate(this.throwable, this.transform.position, Quaternion.identity);
				proj.GetComponent<ProjectileScript>().damage = projDamage;
				Physics2D.IgnoreCollision(this.collider2D, proj.collider2D);

				proj.rigidbody2D.AddForce(new Vector2(70 * projDirection * projspeed, 0));
			}
		} else {
			delayTime2 = 0;
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
		//print("Ow that hurt " + damage + " much");
		this.damage += f;
		this.rigidbody2D.AddForce(new Vector2(0, 12 * Mathf.Sqrt(damage * f))); //I want a function that increases, but slowly. Sqrt fits the requirements
		this.rigidbody2D.AddForce (f * damage * ((Vector2)this.transform.position - placement));
		//this.rigidbody2D.AddForce (new Vector2(0, damage));

	}



}

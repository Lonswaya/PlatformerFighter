using UnityEngine;
using System.Collections;

public class GuiText : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		string text = "";
		GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
		foreach (GameObject p in Players) {
			text += "Player " + p.GetComponent<BallController>().playerid + 
				" " + p.GetComponent<BallController>().damage.ToString("F0") + 
				"% Deaths = " + p.GetComponent<BallController>().deaths + '\n';
		}
		this.guiText.text = text;
	}
}

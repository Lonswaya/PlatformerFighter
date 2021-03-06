﻿using UnityEngine;
using System.Collections;

public class CameraZoomer : MonoBehaviour {
	public float dampTime = 0.15f;
	private Vector3 velocity = Vector3.zero;
	GameObject[] targets;
	public float bufferX = 0, bufferY = 0;
	
	
	void Start() {

	}
	void Update () 
	{
		targets = GameObject.FindGameObjectsWithTag("Player");

		if (targets != null)
		{
			float avgX = 0;
			float avgY = 0;
			int i = 0;
			foreach (GameObject g in targets) {
				avgX += g.transform.position.x;
				avgY += g.transform.position.y;

				i++;
			}
			avgX += GameObject.Find("CenterPlatform").transform.position.x;
			avgY += GameObject.Find("CenterPlatform").transform.position.y;
			i++;
			Vector3 avg = new Vector3(avgX/i, avgY/i, this.transform.position.z);
			dampTime = 1/Vector2.Distance(this.transform.position, avg);
			Vector3 delta = avg - transform.position;
			Vector3 destination = transform.position + delta;												   //destination vector (messy)
			destination.Set (destination.x + bufferX, destination.y + bufferY, destination.z);				   //destination vector (fixed)
			transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, dampTime);  //function to move
			
		}
	}
}

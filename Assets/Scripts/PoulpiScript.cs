﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoulpiScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		this.movement = new Vector2( this.speed.x * this.direction.x, this.speed.y * this.direction.y);
	}
	void FixedUpdate() { 
		// Apply movement to the rigidbody 
		GetComponent<Rigidbody2D>().velocity = movement; 
	}

	public Vector2 speed = new Vector2(10, 10);
	public Vector2 direction = new Vector2(-1, 0);
	private Vector2 movement;
}

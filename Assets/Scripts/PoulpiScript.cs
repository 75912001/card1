using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoulpiScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float x = -Random.Range (0.1f, 0.5f);
		float y = Random.Range (-0.5f, 0.5f);
		movement = new Vector2( this.speed.x * x, this.speed.y * y);
	}
	void FixedUpdate() { 
		// Apply movement to the rigidbody 
		GetComponent<Rigidbody2D>().velocity = movement; 
	}

	public Vector2 speed = new Vector2(1, 1);
	public Vector2 direction = new Vector2(-1, 0);
	private Vector2 movement;
}

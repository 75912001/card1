using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis ("Vertical");
		this.movement = new Vector2 (this.speed.x * inputX, this.speed.y * inputY);

		//shoot
		bool shoot = Input.GetButtonDown("Fire1");
		if (shoot) {
			WeaponScript weaponScript = GetComponent<WeaponScript> ();
			if (null != weaponScript) {
				weaponScript.Attack (false);
			}
		}
	}
	void FixedUpdate() {
		print (this.movement);
		GetComponent<Rigidbody2D>().velocity =this.movement;
	} 

	public Vector2 speed = new Vector2 (50, 50);
	private Vector2 movement;

}

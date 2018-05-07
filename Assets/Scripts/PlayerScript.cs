using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	void Awake(){
		weaponScript = GetComponent<WeaponScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		float inputX = Input.GetAxis ("Horizontal");
		float inputY = Input.GetAxis ("Vertical");
		this.movement = new Vector2 (this.speed.x * inputX, this.speed.y * inputY);

		//shoot
		//bool shoot = Input.GetButtonDown("Fire1");
		//if (shoot) {
			weaponScript.Attack (false);
		//}
	}
	void FixedUpdate() {
		GetComponent<Rigidbody2D>().velocity = movement;
	} 
	void OnCollisionEnter2D(Collision2D collision){
		PoulpiScript poulpi = collision.gameObject.GetComponent<PoulpiScript> ();
		if (null != poulpi) {
			HealthScript poulpiHealth = poulpi.GetComponent<HealthScript> ();
			if (null != poulpiHealth) {
				HealthScript playerHealth = this.GetComponent<HealthScript> ();
				if (null != playerHealth) {
					playerHealth.Damage (poulpiHealth.collisionDamage);
					if (0 < playerHealth.hp) {
						poulpiHealth.Damage (poulpiHealth.hp);
					}
				}
			}
		}
	}

	public Vector2 speed = new Vector2 (5, 5);
	private Vector2 movement;
	public WeaponScript weaponScript;
}

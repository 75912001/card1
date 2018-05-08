using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
	void Awake()
	{
		// Retrieve the weapon only once
		weapons = GetComponentsInChildren<WeaponScript>();
		collider2D = gameObject.GetComponent<Collider2D> ();
		renderer = gameObject.GetComponent<Renderer> ();
	}
	// Use this for initialization
	void Start () {
		Spawn (false);
	}
	
	// Update is called once per frame
	void Update () {
		float x = -Random.Range (0.1f, 0.5f);
		float y = Random.Range (-0.5f, 0.5f);
		movement = new Vector2( this.speed.x * x, this.speed.y * y);

		if (hasSpawn == false){
			if (renderer.isVisibleExt(Camera.main)){
				Spawn(true);
			}
		}else{
			// Auto-fire
			foreach (WeaponScript weapon in weapons){
				if (weapon != null && weapon.enabled && weapon.CanAttack){
					weapon.Attack(true);
				}
			}

			//离开镜头，销毁对象
			if (!renderer.isVisibleExt(Camera.main)){
				Destroy(gameObject);
			}
		}
	}

	void FixedUpdate() { 
		// Apply movement to the rigidbody 
		GetComponent<Rigidbody2D>().velocity = movement; 
	}

	private void Spawn(bool isEnabled){
		hasSpawn = isEnabled;

		// Enable everything
		// -- Collider
		collider2D.enabled = isEnabled;
		// -- Shooting
		foreach (WeaponScript weapon in weapons){   
			weapon.enabled = isEnabled;
		}
	}

	Renderer renderer;
	private bool hasSpawn = false;
	private WeaponScript[] weapons;
	UnityEngine.Collider2D collider2D;
	//移动
	public Vector2 speed = new Vector2(1, 1);
	public Vector2 direction = new Vector2(-1, 0);
	private Vector2 movement;

}

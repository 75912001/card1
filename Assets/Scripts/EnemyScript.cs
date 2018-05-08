using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//敌人
public class EnemyScript : MonoBehaviour {
	void Awake()
	{
		// Retrieve the weapon only once
		this.weaponScripts = GetComponentsInChildren<WeaponScript>();
		if (this.weaponScripts.Length <= 0){
			Debug.LogErrorFormat ("EnemyScript获取WeaponScripts失败");
		}
	}
	// Use this for initialization
	void Start () {
		this.Spawn (false);
	}
	
	// Update is called once per frame
	void Update () {
		//移动
		float x = -Random.Range (0.1f, 0.5f);
		float y = Random.Range (-0.5f, 0.5f);
		this.movement = new Vector2( this.speed.x * x, this.speed.y * y);

		if (!this.hasSpawn){
			Renderer renderer = gameObject.GetComponent<Renderer> ();
			if (null == renderer) {
				Debug.LogErrorFormat ("EnemyScript获取Renderer失败");
			}
			if (renderer.isVisibleExt(Camera.main)){
				//进入摄像头
				Spawn(true);
			}
		}else{
			// Auto-fire
			foreach (WeaponScript weapon in this.weaponScripts){
				if (weapon != null && weapon.enabled && weapon.CanAttack){
					weapon.Attack(true,"Prefabs/Bullet0002");
				}
			}

			//离开镜头，销毁对象
			Renderer renderer = gameObject.GetComponent<Renderer> ();
			if (null == renderer) {
				Debug.LogErrorFormat ("EnemyScript获取Renderer失败");
			}
			if (!renderer.isVisibleExt(Camera.main)){
				Destroy(gameObject);
			}
		}
	}

	void FixedUpdate() {
		//移动
		// Apply movement to the rigidbody 
		GetComponent<Rigidbody2D>().velocity = this.movement; 
	}

	private void Spawn(bool isEnabled){
		this.hasSpawn = isEnabled;

		// Enable everything
		// -- Collider
		Collider2D collider2D = gameObject.GetComponent<Collider2D> ();
		if (null == collider2D) {
			Debug.LogErrorFormat ("EnemyScript获取Collider2D失败");
		}
		collider2D.enabled = isEnabled;
		// -- Shooting
		foreach (WeaponScript weapon in this.weaponScripts){   
			weapon.enabled = isEnabled;
		}
	}

	private bool hasSpawn = false;
	private WeaponScript[] weaponScripts;

	//移动
	public Vector2 speed = new Vector2(1, 1);
	public Vector2 direction = new Vector2(-1, 0);
	private Vector2 movement;

}

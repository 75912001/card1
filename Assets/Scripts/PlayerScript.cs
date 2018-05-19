using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//玩家
public class PlayerScript : MonoBehaviour {

	void Awake(){
		this.weaponScripts = GetComponentsInChildren<WeaponScript> ();
		if (this.weaponScripts.Length <= 0){
			Debug.LogErrorFormat ("获取WeaponScripts失败");
		}
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		{//移动
			float inputX = Input.GetAxis ("Horizontal");
			float inputY = Input.GetAxis ("Vertical");
			this.movement = new Vector2 (this.speed.x * inputX, this.speed.y * inputY);
		}
		{//防止移动出摄像机范围
			var dist = (transform.position - Camera.main.transform.position).z;
			var leftBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, dist)).x;
			var rightBorder = Camera.main.ViewportToWorldPoint (new Vector3 (1, 0, dist)).x;
			var topBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 0, dist)).y;
			var bottomBorder = Camera.main.ViewportToWorldPoint (new Vector3 (0, 1, dist)).y;

			transform.position = new Vector3 (
				Mathf.Clamp (transform.position.x, leftBorder, rightBorder),
				Mathf.Clamp (transform.position.y, topBorder, bottomBorder),
				transform.position.z);
		}
		{//射击
			//bool shoot = Input.GetButtonDown("Fire1");
			//if (shoot) {
			//this.weaponScript.Attack (false, "Prefabs/Bullet0001");
			foreach (WeaponScript weapon in this.weaponScripts) {
				if (null != weapon && weapon.enabled && weapon.CanAttack) {
					weapon.Attack (false, "Prefabs/Bullet0001");
				}
			}
		}
	}
	void FixedUpdate() {
		//移动
		GetComponent<Rigidbody2D>().velocity = this.movement;
	}

	//刚体碰撞
	void OnCollisionEnter2D(Collision2D collision){
		EnemyScript enemy = collision.gameObject.GetComponent<EnemyScript> ();
		if (null != enemy) {
			HealthScript enemyHealth = enemy.GetComponent<HealthScript> ();
			if (null != enemyHealth) {
				HealthScript playerHealth = GetComponent<HealthScript> ();
				if (null != playerHealth) {
					playerHealth.Damage (enemyHealth.collisionDamage);
					if (0 < playerHealth.hp) {
						enemyHealth.Damage (enemyHealth.hp);
					}
				} else {
					Debug.LogErrorFormat ("PlayerScript刚体未找到HealthScript");
				}
			} else {
				Debug.LogErrorFormat ("EnemyScript刚体未找到HealthScript");
			}
		} else {
			Debug.LogErrorFormat ("PlayerScript碰撞了非EnemyScript的刚体");
		}

	}

	//速度
	public Vector2 speed = new Vector2 (5, 5);
	private Vector2 movement;
	public WeaponScript[] weaponScripts;
}

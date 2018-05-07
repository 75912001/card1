using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		shootCoolDown = 0f;
	}
	
	// Update is called once per frame
	void Update () {
		if (0 < shootCoolDown) {
			shootCoolDown -= Time.deltaTime;
		}
	}
	public bool CanAttack{
		get{
			return shootCoolDown <= 0f;
		}
	}
	public void Attack(bool isEnemy){
		if (!CanAttack) {
			return;
		}
		shootCoolDown = shootingRate;

		if (isEnemy) {
			GameObject bulletPrefabs = (GameObject)Resources.Load ("Prefabs/Bullet0002");
			GameObject bulletPrefab = Instantiate (bulletPrefabs, transform.position, transform.rotation);

			BulletScript bulletScript = bulletPrefab.GetComponent<BulletScript> ();
			if (null == bulletScript) {
				return;
			}
			bulletScript.isEnemyShot = isEnemy;

			BulletMoveScript bulletMoveScript = bulletPrefab.GetComponent<BulletMoveScript> ();
			if (null == bulletMoveScript) {
				return;
			}
			bulletMoveScript.direction = transform.right;
			bulletMoveScript.direction.x = -1;
		} else {
			GameObject bulletPrefabs = (GameObject)Resources.Load ("Prefabs/Bullet0001");
			GameObject bulletPrefab = Instantiate (bulletPrefabs, transform.position, transform.rotation);

			BulletScript bulletScript = bulletPrefab.GetComponent<BulletScript> ();
			if (null == bulletScript) {
				return;
			}
			bulletScript.isEnemyShot = isEnemy;

			BulletMoveScript bulletMoveScript = bulletPrefab.GetComponent<BulletMoveScript> ();
			if (null == bulletMoveScript) {
				return;
			}
			bulletMoveScript.direction = transform.right;
			bulletMoveScript.direction.x = 1;
		}
	}
	public float shootingRate = 0.25f;
	public float shootCoolDown;
}

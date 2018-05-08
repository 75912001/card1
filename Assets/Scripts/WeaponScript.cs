using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
public class WeaponScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		//更新射击冷却时间
		if (0 < shootCoolDown) {
			shootCoolDown -= Time.deltaTime;
		}
	}

	//是否可以攻击
	public bool CanAttack{
		get{
			return shootCoolDown <= 0f;
		}
	}

	//攻击
	public void Attack(bool isEnemy, string bulletName){
		if (!CanAttack) {
			return;
		}
		shootCoolDown = shootingRate;

		if (isEnemy) {
			GameObject bulletPrefabs = (GameObject)Resources.Load ("Prefabs/Bullet0002");
			GameObject bulletPrefab = Instantiate (bulletPrefabs, transform.position, transform.rotation);

			BulletScript bulletScript = bulletPrefab.GetComponent<BulletScript> ();
			if (null == bulletScript) {
				Debug.LogErrorFormat ("查找子弹失败{0}", bulletName);
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
	//射击 秒
	public float shootingRate = 0.25f;
	//射击冷却时间 秒
	public float shootCoolDown = 0f;
}

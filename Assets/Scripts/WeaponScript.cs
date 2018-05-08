using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//开火
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

		GameObject bulletPrefabs = (GameObject)Resources.Load (bulletName);
		GameObject bulletPrefab = Instantiate (bulletPrefabs, transform.position, transform.rotation);
		BulletScript bulletScript = bulletPrefab.GetComponent<BulletScript> ();
		if (null == bulletScript) {
			Debug.LogErrorFormat ("查找BulletScript失败{0}", bulletName);
			return;
		}
		bulletScript.isEnemyShot = isEnemy;
		BulletMoveScript bulletMoveScript = bulletPrefab.GetComponent<BulletMoveScript> ();
		if (null == bulletMoveScript) {
			Debug.LogErrorFormat ("查找BulletMoveScript失败{0}", bulletName);
			return;
		}
		bulletMoveScript.direction = transform.right;
		if (isEnemy) {
			bulletMoveScript.direction.x = -1;
		} else {
			bulletMoveScript.direction.x = 1;
		}
	}
	//射击 秒
	public float shootingRate = 0.25f;
	//射击冷却时间 秒
	public float shootCoolDown = 0f;
}

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

		GameObject bulletPrefabs = (GameObject)Resources.Load("Prefabs/Bullet0001");
		//var bulletTranform = Instantiate(bulletPrefab.transform) as Transform;
		//bulletTranform.position = transform.position;
		//print ("WeaponScript");
		//print (bulletTranform.position);

		GameObject bulletPrefab = Instantiate (bulletPrefabs, transform.position, transform.rotation);
		//bulletPrefab.transform.position = transform.position;
		//Debug.Log(bulletPrefab.transform.position);

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
	}
	public Transform bulletPrefab;
	public float shootingRate = 0.25f;
	private float shootCoolDown;
}

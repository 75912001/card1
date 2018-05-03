using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void Damage(int damageCount){
		hp -= damageCount;
		if (hp <= 0) {
			Destroy (gameObject);
		}
	}
	void OnTriggerEnter2D(Collider2D otherCollider){
		BulletScript bulletScript = otherCollider.gameObject.GetComponent<BulletScript> ();
		if (null == bulletScript) {
			return;
		}
		if (isEnemy == bulletScript.isEnemyShot) {
			return;
		}
		Damage (bulletScript.damage);
		Destroy (bulletScript.gameObject);

	}
	public int hp = 1;
	public bool isEnemy = true;
}

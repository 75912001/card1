using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//健康
public class HealthScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//伤害(hp<=0时销毁)
	public void Damage(int damageCount){
		this.hp -= damageCount;
		if (this.hp <= 0) {
			Destroy (this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D otherCollider){
		{//敌方子弹造成伤害，并销毁子弹
			BulletScript bulletScript = otherCollider.gameObject.GetComponent<BulletScript> ();
			if (null == bulletScript) {
				return;
			}
			if (this.isEnemy == bulletScript.isEnemyShot) {
				return;
			}
			this.Damage (bulletScript.damage);
			Destroy (bulletScript.gameObject);
		}
	}
	//血量
	public int hp = 10;
	//血量最大值
	public int hp_max = 10;
	//阵营
	public bool isEnemy = true;
	//碰撞给对方造成的伤害
	public int collisionDamage = 1;
}

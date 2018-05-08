using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//子弹
public class BulletScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//n秒后自动销毁
		Destroy(gameObject, 10);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	//伤害值
	public int damage = 1;
	public bool isEnemyShot = false;
}

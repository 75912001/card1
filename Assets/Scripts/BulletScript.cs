using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Destroy(gameObject, 10);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public int damage = 1;
	public bool isEnemyShot = false;
}

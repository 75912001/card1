using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeaponScript : MonoBehaviour {
	// Update is called once per frame
	void Update () {
		//weaponScript.Attack (true);
	}

	void Awake(){
		weaponScript = GetComponentInChildren<WeaponScript> ();

		weaponScript.shootingRate = Random.Range (1f, 5f);
		//weaponScript.shootCoolDown = Random.Range (10f, 50f);
	}

	private WeaponScript weaponScript;
}

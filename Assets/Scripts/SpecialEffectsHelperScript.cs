using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialEffectsHelperScript : MonoBehaviour {

	void Awake(){
		if (null != Instance) {
			Debug.LogErrorFormat ("SpecialEffectsHelperScript 初始化多个");
		}
		Instance = this;
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Explosion(Vector3 position){
		this.instantiate (this.smokeEffect, position);
	}

	public static SpecialEffectsHelperScript Instance;
	public ParticleSystem smokeEffect;
	//私有
	private ParticleSystem instantiate(ParticleSystem prefab, Vector3 position){
		ParticleSystem newParticleSystem = Instantiate (prefab, position, Quaternion.identity) as ParticleSystem;
		Destroy(newParticleSystem.gameObject, newParticleSystem.main.startLifetimeMultiplier);
		return newParticleSystem;
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//子弹移动
public class BulletMoveScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		switch (this.moveType) {
		case 1:
			this.movement = new Vector2 (this.speed.x * this.direction.x, this.speed.y * this.direction.y);
			break;
		default:
			Debug.LogErrorFormat ("子弹 移动类型{0}", this.moveType);
			break;
		}
	}
	void FixedUpdate() { 
		// Apply movement to the rigidbody 
		GetComponent<Rigidbody2D>().velocity = this.movement; 
	}

	//速度
	public Vector2 speed = new Vector2(10, 0);
	//方向
	public Vector2 direction = new Vector2(1, 0);
	private Vector2 movement;

	//移动的类型
	//1.直线
	public int moveType = 1;
}

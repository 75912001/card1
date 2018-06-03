using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//滚动
public class ScrollingScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (this.isLooping){
			//拥有Renderer(渲染器)的列表
			this.childList = new List<Transform>();
			for (int i = 0; i < transform.childCount; i++){
				Transform child = transform.GetChild(i);
				Renderer renderer = child.gameObject.GetComponent<Renderer> ();
				if (null != renderer) {
					this.childList.Add (child);
					print (child.position.x);
					print (child.position.y);
				} else {
					Debug.LogErrorFormat ("ScrollingScript未找到Renderer");
				}
			}

			this.childList.Sort((x, y) => x.position.x.CompareTo(y.position.x));//position.x升序
			//this.childList.Sort((x, y) => -x.position.x.CompareTo(y.position.x));//position.x降序

			print (this.childList.Count);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 movement = new Vector3(this.speed.x*this.direction.x, 
			this.speed.y*this.direction.y,0);
		movement *= Time.deltaTime;
		transform.Translate (movement);
		if (this.isLinkedToCamera) {
			Camera.main.transform.Translate (movement);
		}

		if (this.isLooping) {
			if (0 < this.childList.Count) {
				Transform firstChild = this.childList[0];
				// Check if the child is already (partly) before the camera.
				// We test the position first because the IsVisibleFrom
				// method is a bit heavier to execute.
				if (firstChild.position.x < Camera.main.transform.position.x) {
					// If the child is already on the left of the camera,
					// we test if it's completely outside and needs to be
					// recycled.
					Renderer firstRenderer = firstChild.gameObject.GetComponent<Renderer> ();
					// Add only the visible children
					if (null != firstRenderer) {
						if (!firstRenderer.isVisibleExt (Camera.main)) {
							// Get the last child position.
							Transform lastChild = childList [childList.Count - 1];
							Renderer lastRenderer = lastChild.gameObject.GetComponent<Renderer> ();
							if (null != lastRenderer) {
								Vector3 lastPosition = lastChild.transform.position;
								Vector3 lastSize = (lastRenderer.bounds.max - lastRenderer.bounds.min);
								// Set the position of the recyled one to be AFTER
								// the last child.
								// Note: Only work for horizontal scrolling currently.
								print (this.childList.Count);
								print (firstChild.position.x);
								print (firstChild.position.y);
								firstChild.position = new Vector3 (lastPosition.x + lastSize.x, firstChild.position.y, firstChild.position.z);
								print (firstChild.position.x);
								print (firstChild.position.y);
								// Set the recycled child to the last position
								// of the backgroundPart list.
								childList.Remove (firstChild);
								childList.Add (firstChild);
								print (this.childList.Count);

								/////////////////////////
								/// 创建新的敌人
								//Debug.LogErrorFormat ("!!!!!!");
								{	string name = "Prefabs/Poulpi0001";
									GameObject prefabs = (GameObject)Resources.Load (name);
									GameObject prefab = Instantiate (prefabs, transform.position, transform.rotation);
									{
										EnemyScript script = prefab.GetComponent<EnemyScript> ();
										if (null == script) {
											Debug.LogErrorFormat ("查找script失败{0}", name);
											return;
										}
										script.speed.x = 1;
										script.speed.y = 1;
										script.direction.x = -1;
									}
									{
										HealthScript script = prefab.GetComponent<HealthScript> ();
										if (null == script) {
											Debug.LogErrorFormat ("查找script失败{0}", name);
											return;
										}
										script.hp = 1;
										script.hp_max = 10;
										script.isEnemy = true;
										script.collisionDamage = 1;
									}
									{
										prefab.transform.position = new Vector3 (20, 3, 50);
									}
								}
							} else {
								Debug.LogErrorFormat ("lastChild未找到Renderer");
							}
						}
					} else {
						Debug.LogErrorFormat ("firstChild未找到Renderer");
					}
				}
			}
		}
	}

	public Vector2 speed = new Vector2(2,0);
	public Vector2 direction = new Vector2(-1, 0);
	public bool isLinkedToCamera = false;
	//是否循环
	public bool isLooping = false;
	//拥有Renderer(渲染器)的 升序 
	//按照position.x 进行升序排序
	private List<Transform> childList;
}

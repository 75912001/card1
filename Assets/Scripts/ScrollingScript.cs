using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if (isLooping){
			//拥有Renderer(渲染器)的列表
			childList = new List<Transform>();
			for (int i = 0; i < transform.childCount; i++){
				Transform child = transform.GetChild(i);
				Renderer renderer = child.gameObject.GetComponent<Renderer> ();
				if (null != renderer){
					childList.Add(child);
					print (child.position.x);
					print (child.position.y);
				}
			}
			childList.Sort((x, y) => x.position.x.CompareTo(y.position.x));//position.x升序
			//backGroundPart.Sort((x, y) => -x.position.x.CompareTo(y.position.x));//position.x降序

			print (childList.Count);
		}
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 movement = new Vector3(speed.x*direction.x, speed.y*direction.y,0);
		movement *= Time.deltaTime;
		transform.Translate (movement);
		if (isLinkedToCamera) {
			Camera.main.transform.Translate (movement);
		}

		if (isLooping) {
			if (0 < childList.Count) {
				Transform firstChild = childList[0];
				// Check if the child is already (partly) before the camera.
				// We test the position first because the IsVisibleFrom
				// method is a bit heavier to execute.
				if (firstChild.position.x < Camera.main.transform.position.x) {
					// If the child is already on the left of the camera,
					// we test if it's completely outside and needs to be
					// recycled.
					Renderer firstRenderer = firstChild.gameObject.GetComponent<Renderer> ();
					// Add only the visible children
					if (null != firstRenderer){
						if (!firstRenderer.isVisibleExt (Camera.main)) {
							// Get the last child position.
							Transform lastChild = childList[childList.Count - 1];
							Renderer lastRenderer = lastChild.gameObject.GetComponent<Renderer> ();
							if (null != lastRenderer) {
								Vector3 lastPosition = lastChild.transform.position;
								Vector3 lastSize = (lastRenderer.bounds.max - lastRenderer.bounds.min);
								// Set the position of the recyled one to be AFTER
								// the last child.
								// Note: Only work for horizontal scrolling currently.
								print (childList.Count);
								print(firstChild.position.x);
								print (firstChild.position.y);
								firstChild.position = new Vector3 (lastPosition.x + lastSize.x, firstChild.position.y, firstChild.position.z);
								print(firstChild.position.x);
								print (firstChild.position.y);
								// Set the recycled child to the last position
								// of the backgroundPart list.
								childList.Remove (firstChild);
								childList.Add (firstChild);
								print (childList.Count);
							}
						}
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// For infinite background only
		if (isLooping){
			// Get all the children of the layer with a renderer
			backGroundPart = new List<Transform>();
			for (int i = 0; i < transform.childCount; i++){
				Transform child = transform.GetChild(i);
				Renderer renderer = child.gameObject.GetComponent<Renderer> ();
				// Add only the visible children
				if (null != renderer){
					backGroundPart.Add(child);
				}
			}
			// Sort by position.x
			// Note: Get the children from left to right.
			// We would need to add a few conditions to handle
			// all the possible scrolling directions.
			backGroundPart.Sort((x, y) => x.position.x.CompareTo(y.position.x));//升序
			//backGroundPart.Sort((x, y) => -x.position.x.CompareTo(y.position.x));//降序
		}
	}
	
	// Update is called once per frame
	void Update () {
		//
		Vector3 movement = new Vector3(speed.x*direction.x, speed.y*direction.y,0);
		movement *= Time.deltaTime;
		transform.Translate (movement);
		if (isLinkedToCamera) {
			Camera.main.transform.Translate (movement);
		}

		if (isLooping) {
			if (0 < backGroundPart.Count) {
				Transform firstChild = backGroundPart[0];
				Transform lastChild = backGroundPart[backGroundPart.Count - 1];
				// Check if the child is already (partly) before the camera.
				// We test the position first because the IsVisibleFrom
				// method is a bit heavier to execute.
				if (firstChild.position.x < Camera.main.transform.position.x) {
					// If the child is already on the left of the camera,
					// we test if it's completely outside and needs to be
					// recycled.
					Renderer firstRenderer = firstChild.gameObject.GetComponent<Renderer> ();
					Renderer lastRenderer = lastChild.gameObject.GetComponent<Renderer> ();

					// Add only the visible children
					if (null != firstRenderer && null != lastRenderer){
						print ("1");
						if (!firstRenderer.isVisibleFrom (Camera.main)) {
							print ("2");
							// Get the last child position.
							Vector3 lastPosition = lastChild.transform.position;
							Vector3 lastSize = (lastRenderer.bounds.max - lastRenderer.bounds.min);
							// Set the position of the recyled one to be AFTER
							// the last child.
							// Note: Only work for horizontal scrolling currently.
							firstChild.position = new Vector3 (lastPosition.x + lastSize.x, firstChild.position.y, firstChild.position.z);

							// Set the recycled child to the last position
							// of the backgroundPart list.
							backGroundPart.Remove (firstChild);
							backGroundPart.Add (firstChild);
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
	private List<Transform> backGroundPart;
}

using UnityEngine;
using System.Collections;

public class DragAndDropMeItem : MonoBehaviour {
	public float moveSpeed;
	public float offset = 0.05f;
	private bool following;
	private bool collidedWithSlot;
	// Use this for initialization
	void Start () {
		following = false;
		offset += 10;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0) && isInRange())
		{
			following = !following;
		}
		followMouse ();
	}

	bool isInRange(){
		return (Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position).magnitude <= offset;
	}

	bool isOverSlot() {
		return collidedWithSlot;
	}

	void followMouse() {
		if (following) {
			transform.position = Vector2.Lerp (transform.position, Camera.main.ScreenToWorldPoint (Input.mousePosition), moveSpeed);
		} else if (isOverSlot ()) {
			
		}
	}
}


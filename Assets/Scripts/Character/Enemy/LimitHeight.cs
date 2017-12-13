using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitHeight : MonoBehaviour {

	public float minHeight;

	private Vector3 limit;

	void Start () {
		limit = new Vector3 (transform.position.x, minHeight, transform.position.z);
	}

	void Update () {
		if (transform.position.y <= minHeight) {
			limit.x = transform.position.x;
			limit.z = transform.position.z;
			transform.position = limit;
		}
	}
}
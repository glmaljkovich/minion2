using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour
{
	public float hardness;
	protected Rigidbody2D myBody;
	protected Animator anim;
	private float lastHit = 0f;
	public float hitCoolDown;

	// Use this for initialization
	void Start ()
	{
		myBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (mined ()) {
			myBody.bodyType = RigidbodyType2D.Dynamic;
			transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		}
	}

	public void mine(float toolPower){
		if (!mined () && lastHit <= 0) {
			hardness -= toolPower;
			lastHit = hitCoolDown;
		} else {
			lastHit -= Time.deltaTime;
		}
	
	}

	public bool mined(){
		return hardness <= 0;
	}


}


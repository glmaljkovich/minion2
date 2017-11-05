using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovementInY : MonoBehaviour {

	[Range(1, 100)]
	public float jumpSpeed;
	public bool grounded;
	public bool jumpBoostGround;


	private Vector2 gravity;
	private float speed;
	private Animator anim;
	private bool isDead = false;
	private Rigidbody2D rb;
	private Player player;


	void Start () {
		speed = jumpSpeed;
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		player = GetComponent<Player> ();
		gravity = new Vector2 (0, -4f);
	}

	void Update () {
		if (!player.isDead()) {
			checkForMove();
		}

		if (!grounded) {
			rb.AddForce (gravity);
		}
	}

	private void checkForMove(){
		checkForJump ();
		checkForBend ();
	}

	public void checkForBend(){
		if (Input.GetKey(KeyCode.DownArrow) && grounded) {
			anim.SetTrigger ("Bend");
		}
	}

	public void checkForJump(){
		if (Input.GetKey(KeyCode.Space) && grounded) {
			changeSpeed ();
			rb.velocity = Vector2.up * speed;
			anim.SetTrigger ("Jump");
			grounded = false;
			jumpBoostGround = false;
		}
	}

	private void changeSpeed() {
		float factor = jumpBoostGround ? 1.5f : 1;
		speed = jumpSpeed * factor;
	}

	public void OnCollisionEnter2D (Collision2D collision) {
		switch (collision.gameObject.tag) {
		case "ground":
			grounded = true;
			jumpBoostGround = false;
			break;
		case "impulseGround":
			grounded = true;
			jumpBoostGround = true;
			break;
		default:
			break;
		}
	}


}

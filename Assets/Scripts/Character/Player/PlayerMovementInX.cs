using UnityEngine;
using System.Collections;

public class PlayerMovementInX : MonoBehaviour {
	
	[Range(3, 6)]
	public float runSpeed;

	[Range(1, 2)]
	public float walkSpeed;

	public float stepsCd;
	public bool isDead = false;

	private float currentCd;
	private bool isRight;
	private Rigidbody2D rb;
	private Animator anim;
	private Player player;

	void Start () {
		isRight = true;
		rb = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		player = GetComponent<Player> ();
		currentCd = stepsCd;
	}

	void FixedUpdate () {
		if (!player.isDead()) {
			move ();
			flip ();
		}
	}

	public bool getIsRight(){
		return this.isRight;
	}
		
	private void move(){
		var movement = new Vector3 (Input.GetAxis ("Horizontal"), Input.GetAxis ("Vertical"), 0);
		if (shouldMove()) {
			if (Input.GetKey (KeyCode.LeftShift))
				run (movement);
			else
				walk (movement);
		} else
			idle ();

		currentCd += Time.deltaTime;
	}

	private void run(Vector3 movement){
		anim.SetTrigger ("Run");
		movePlayer (movement, runSpeed);
	}

	private void walk(Vector3 movement){
		anim.SetTrigger ("Walk");
		movePlayer (movement, walkSpeed);
	}

	private void idle(){
		anim.SetTrigger ("Idle");
	}

	private void movePlayer(Vector3 movement, float speed) {
		transform.position += movement * speed * Time.deltaTime;
	}

	private bool shouldMove(){
		return Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.LeftArrow);
	}

	private void flip(){
		float horizontal = Input.GetAxis ("Horizontal");
		if (horizontal > 0 && !isRight || horizontal < 0 && isRight) {
			isRight = !isRight;
			Vector3 scale = transform.localScale;
			scale.x *= -1;
			transform.localScale = scale;
		}
	}

}

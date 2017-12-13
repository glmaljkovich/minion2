using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BubbleLoot : MonoBehaviour {

	[Range(0, 3)]
	public float distance;

	[Range(1, 3)]
	public float speed;
	public AudioClip soundEffect;
	protected float value;
	private bool give = false;

	protected FloatingTextController textController;
	protected Player player;

	protected AudioSource audioSource;
	private Vector3 target;
	private Rigidbody2D rb;
	private float originalGravity;

	void Start() {
		textController = GameObject.FindObjectOfType<FloatingTextController> ();
		player = GameObject.FindObjectOfType<Player> ();
		audioSource = GetComponent<AudioSource>();
		rb = GetComponent<Rigidbody2D>();
		originalGravity = rb.gravityScale;
	}

	void Update() {
		checkFollow ();
	}

	public void checkFollow() {
		float range = Vector2.Distance (transform.position, player.transform.position);
		 
		if (range <= distance) {
			target = player.transform.position;
			target.y += 0.5f;
			if(rb.gravityScale != 0f) {
				rb.gravityScale = 0f;
			}
			transform.position = Vector2.MoveTowards (transform.position, target, speed * Time.deltaTime);
		} else {
			rb.gravityScale = 1f;
		}
	}


	public void setValue(float value){
		this.value = value;
	}

	public void OnCollisionStay2D (Collision2D collision) {
		if (collision.gameObject.name == "Player" && !give) {
			takeValue (collision.gameObject.GetComponent<Player>());
			applySound ();
			Destroy(gameObject);
			give = true;
		}
	}

	public virtual void applySound() {
		audioSource.PlayOneShot(soundEffect, 1f);
	}

	public virtual void takeValue (Player player){}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour {
	public float damage = 20f;
	public float defense = 0f;
	protected Rigidbody2D myBody;
	protected Animator anim;
	protected Health health;

	public Character() {}

	public bool isDead(){
		return this.health.health <= 0;
	}

	void Start () {
		myBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
	}

	public void takeDamage(float damage){
		health.takeDamage(damage - defense);
	}

	void Update () {}

	public void die(){
			gameObject.layer = 12;
			health.hide ();
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character {

	private SpriteRenderer[] spritesRenderers;
	private Energy energy;
	private float  lastHit = 0f;
	private float  secondsOfInvulnerability = 2f;
	private bool deadAnim = false;

	public Inventory inventory;
	public float powerHurtForce = 2f;
	public float hurtForce = 10f;
	public float timeOfDead = 1f;

	private CameraController camera;

	void Start () {
		health = gameObject.GetComponent<Health>();
		energy = gameObject.GetComponent<Energy>();
		anim = GetComponent<Animator> ();
		camera = gameObject.GetComponentInChildren<CameraController>();
		spritesRenderers = gameObject.GetComponentsInChildren<SpriteRenderer>();
	}

	void Update () {
		if (!isDead ()) {

		} else {
			updateDeath ();
		}


	}

	public void checkVulnerability(){
		lastHit -= Time.deltaTime;

		foreach (SpriteRenderer sprite in spritesRenderers) {
			Color color = sprite.color;
			color.a = (lastHit > 0) ? Mathf.Sin (Random.Range(-1f, 1f)) : 1f;
			sprite.material.color = color;
		}
	}

	public void updateDeath () {
		timeOfDead -= Time.deltaTime;

		if (!deadAnim) {
			anim.SetTrigger ("Die");
			deadAnim = true;
		}

		if (timeOfDead > 0) {
			foreach (SpriteRenderer sprite in spritesRenderers) {
				Color color = sprite.color;
				color.a = timeOfDead;
				sprite.material.color = color;
			}
		} else {
			this.die ();
		}
	}

	public void takeDamage(float damage, Transform transformE){
		hurtEffect (transformE);
		if (lastHit <= 0) {
			lastHit = secondsOfInvulnerability;
			base.takeDamage (damage);
			camera.takeDamage ();
		}
	}

	public void hurtEffect(Transform transformE){
		float side = (transformE.localScale.x > 0) ? 1 : -1;

		Vector3 hurtVector = new Vector3(side  * powerHurtForce, 0, 0);
		GetComponent<Rigidbody2D>().AddForce(hurtVector * hurtForce);
	}

	public float getToolPower() {
		return damage;
	}

	public void Die(){
		this.health.takeDamage (1000);
		this.myBody.simulated = false;
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

	public SpriteRenderer spriteRenderer;
	public float deadSpeed = 0.0000000000001f;
	public float timeOfDead = 1f;
	public int experience;
	public float cd = 0.5f;
	public float lastTime = 0.5f;
	public bool giveLoot;
	public string type = "enemy";

	public AudioClip hitSound;
	public AudioClip takeDamageSound;
	public AudioClip normalSound;
	public AudioClip dieSound;

	private AudioSource source;
	private Player player;
	private EnemyAI enemyAI;
	private LootSystem lootSystem;
	private FloatingTextController textController;

	
	void Start () {
		health 				= gameObject.GetComponent<Health>();
		enemyAI 			= gameObject.GetComponent<EnemyAI>();
		spriteRenderer 		= gameObject.GetComponent<SpriteRenderer>();
		player 				= GameObject.FindObjectOfType<Player> ();
		textController 		= GameObject.FindObjectOfType<FloatingTextController> ();
		lootSystem 			= GameObject.FindObjectOfType<LootSystem> ();

		giveLoot = false;
		source = GetComponent<AudioSource> ();
		source.loop = true;
		source.clip = normalSound;
		source.Play();
	}

	void Update () {
		checkDead ();
		lastTime += Time.deltaTime;
	}
		
	public void checkDead(){
		if(isDead()) {
			Color color = spriteRenderer.material.color;
			if (timeOfDead > 0) {
				color.a = timeOfDead;
				timeOfDead -= Time.deltaTime;

				spriteRenderer.material.color = color;
			} else {
				this.die ();
				loot ();
				Destroy(gameObject, 1f);
			}
		}
	}

	public void loot() {
		if (!giveLoot) {
			source.PlayOneShot (dieSound, 0.5f);
			lootSystem.loot (gameObject.transform);
			giveLoot = true;
		}
	}

	public void move(){
	}

	public void takeDamage(float damage) {
		source.PlayOneShot (takeDamageSound, 0.5f);
		base.takeDamage (damage);
	}

	public void checkForAnimation(){
	}

	public void attack(Player player){
		player.takeDamage(Random.Range(damage * 0.8f, damage), transform);
	}

	// TODO: Refactorizar esto a un Collision Stay con cooldown
	public void OnCollisionStay2D (Collision2D collision) {
		if (collision.gameObject.name == "Player" && lastTime >= cd) {
			attack (collision.gameObject.GetComponent<Player> ());
			source.PlayOneShot (hitSound, 1f);
			lastTime = 0;
		}
	}
}

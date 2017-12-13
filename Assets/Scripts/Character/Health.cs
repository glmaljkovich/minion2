using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour
{	
	public float health = 100f;					// The player's health.
	public float maxHealth = 100f;
	public Character character;
	public float repeatDamagePeriod = 2f;		// How frequently the player can be damaged.
	public StatBar healthBar;			// Reference to the sprite renderer of the health bar.
	private float lastHitTime;					// The time at which the player was last hit.

	void Awake () {
		character = gameObject.GetComponent<Character> (); 
		health = maxHealth;
	}

	void Update(){
		if (health <= 0) {
			character.die ();
		}
		UpdateHealthBar ();
	}

	public void revive() {
		health = maxHealth;
		(healthBar as PlayerHealthBar).revive (); 
	}

	public void takeDamage (float damage) {
		if (health > 0f && damage > 0) {
			health = Mathf.Max (0, health - damage);
		}
	}

	public void hide(){
		healthBar.hide();
	}

	public void show(){
		healthBar.show();
	}

	public void UpdateHealthBar(){
		healthBar.updateBar (health, maxHealth);
	}

	public void addMaxHp(float hp){
		maxHealth += hp;
	}

	public void addHP(float hp) {
		if (health + hp >= maxHealth) {
			health = maxHealth;
		} else {
			health += hp;
		}
	}

	public void die(){
		health = 0;
	}
}

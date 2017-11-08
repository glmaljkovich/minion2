using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Health : MonoBehaviour
{	
	public float health = 100f;					// The player's health.

	public Vector2 scale;
	public Character character;
	public float repeatDamagePeriod = 2f;		// How frequently the player can be damaged.
	public Image healthBar;			// Reference to the sprite renderer of the health bar.
	private float lastHitTime;					// The time at which the player was last hit.
	private Vector3 healthScale;				// The local scale of the health bar initially (with full health).
	private Animator anim;						// Reference to the Animator on the player


	void Awake () {
		anim = GetComponent<Animator>();
		character = gameObject.GetComponent<Character> (); 
		healthScale = healthBar.transform.localScale;
	}

	void Update(){
		if (health <= 0) {
			character.die ();
		}
	}

	public void takeDamage (float damage) {
		if (health > 0f && damage > 0) {
			health -= damage;
			UpdateHealthBar();
		}
	}

	public void hide(){
		healthBar.enabled = false;
	}

	public void show(){
		healthBar.enabled = true;
	}
		
	public void UpdateHealthBar () {
		//healthBar.material.color = Color.Lerp(healthBar.material.color, Color.red, 1 - health * 0.01f);
		healthBar.transform.localScale = new Vector3(healthScale.x * health * 0.01f, scale.y, scale.x);
	}
}

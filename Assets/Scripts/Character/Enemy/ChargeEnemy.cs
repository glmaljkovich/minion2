using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargeEnemy : MonoBehaviour {

	public float radiusOfCharge;
	public float radiusOfFollow;
	public float normalSpeed;
	public float chargeSpeed;
	public float boundHeight;

	private GameObject player;
	private EnemyAI ai;

	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
		ai = gameObject.GetComponent<EnemyAI> ();
	}

	void Update () {
		if (ai.isEnemyDetected () && !player.GetComponent<Player>().isDead()) {
			checkForMovement ();	
		}
	}

	public void checkForMovement() {
		float range = Vector2.Distance (transform.position, player.transform.position);

		flipToPlayer ();

		if (range <= radiusOfCharge) {
			charge ();
		} else {
			follow ();
		} 
	}

	public void follow(){
		transform.position = Vector2.MoveTowards(transform.position, player.transform.position, normalSpeed * Time.deltaTime);
	}

	public void charge() { 
		transform.position = Vector2.MoveTowards(transform.position, player.transform.position, chargeSpeed * Time.deltaTime);
	}

	public void flipToPlayer (){
		float myX = transform.position.x ;
		float playerX = player.transform.position.x;
		float myScaleX = transform.localScale.x;

		if (myX > playerX && myScaleX > 0 || myX < playerX && myScaleX < 0)
			ai.flip ();
	}
}

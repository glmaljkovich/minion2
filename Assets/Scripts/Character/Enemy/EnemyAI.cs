using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour {


	public int radiusOfMovement = 2;
	public float radiusOfVision = 2f;
	public float enemySpeed = 1f;
	public bool isFacingRight;
	public bool moveRight = true;
	public bool enemyDetected = false;

	private Animator anim;
	private Vector2 startPos;
	private Vector2 endPos;
	private Rigidbody2D enemyRigidBody2D;
	private GameObject player;
	private Enemy enemy;


	void Start () {
		enemyRigidBody2D = GetComponent<Rigidbody2D>();
		player = GameObject.FindGameObjectWithTag("Player");
		anim = GetComponent<Animator> ();
		enemy = GetComponent<Enemy> ();
	}

	public void Awake() {
		startPos = new Vector2(transform.position.x, transform.position.y);
		endPos = new Vector2(startPos.x + radiusOfMovement, startPos.y);
		isFacingRight = transform.localScale.x > 0;
	}
		
	public void Update() {

		detectPlayer ();
		checkSide ();

		if (!enemyDetected) {
			normalMovement ();
		}
			
		//if (outOfRange () && !enemyDetected) {
		//	returnToInitialPos ();
		//}
	}

	public bool inRange() {
		return between (transform.position.x, startPos.x, endPos.x) ||
			between (transform.position.y, startPos.y, endPos.y);
	}

	public void returnToInitialPos(){
		transform.position = Vector2.MoveTowards(transform.position, startPos, enemySpeed * Time.deltaTime);
	}

	public void normalMovement(){
		if (!enemyDetected) {
			if (moveRight) {
				enemyRigidBody2D.velocity = new Vector2 (enemySpeed, 0);
				if (!isFacingRight)
					flip ();
			} else {
				enemyRigidBody2D.velocity = new Vector2 (-enemySpeed, 0);
				if (isFacingRight)
					flip ();
			}
		}
	}

	public void checkSide(){
		if (enemyRigidBody2D.position.x >= endPos.x)
			moveRight = false;
		else if (enemyRigidBody2D.position.x <= startPos.x)
			moveRight = true;
	}

	public void detectPlayer(){
		float range = Vector2.Distance (transform.position, player.transform.position);

		if (range <= radiusOfVision) {
			enemyDetected = true;
		} else
			enemyDetected = false;
	}

	public void flip() {
		transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
		isFacingRight = transform.localScale.x > 0;
	}

	public void takeDamage(){
		enemyDetected = true;
	}

	public bool between(float point, float from, float to) {
		return (point >= from) && (point <= to);
	}

	public bool isEnemyDetected() {
		return enemyDetected;
	}

}

using UnityEngine;
using System.Collections;


public class Block : MonoBehaviour
{
	public float hardness = 20;
	public BlockType type;
	public int spawnCount;
	protected Rigidbody2D myBody;
	protected Animator anim;
	protected Collider2D col;
	private float lastHit = 0f;
	public float hitCoolDown;

	// Use this for initialization
	void Start ()
	{
		myBody = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		col = GetComponent<Collider2D> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (mined ()) {
			myBody.bodyType = RigidbodyType2D.Dynamic;
			transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
		}
	}

	public void mine(float toolPower){
		if (!mined () && lastHit <= 0) {
			hardness -= toolPower;
			lastHit = hitCoolDown;
		} else {
			lastHit -= Time.deltaTime;
		}
	
	}

	public bool mined(){
		return hardness <= 0;
	}

	void OnCollisionEnter2D(Collision2D other) {
		Player player = other.gameObject.GetComponent<Player> ();
		if (mined () && player != null) {
			player.inventory.AddBlocks (type, spawnCount);
			spawnCount = 0;
			Destroy (gameObject);
		}
	}


}


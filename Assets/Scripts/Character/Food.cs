using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour
{
	public float foodPoints = 10;
	bool pointsGiven = false;
	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}

	void OnCollisionEnter2D(Collision2D collision) {
		Player player = collision.gameObject.GetComponent<Player> ();
		if (player != null && !pointsGiven) {
			player.GetComponent<Hunger> ().addFood (foodPoints);
			pointsGiven = true;
			Destroy (this.gameObject);
		}
	}
}


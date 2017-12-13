using UnityEngine;
using System.Collections;

public class Hunger : MonoBehaviour
{
	public float food = 100f;
	public float maxFood = 100f;
	Health health;
	public LinearStatBar hungerBar;
	// Use this for initialization
	void Start ()
	{
		health = this.GetComponent<Health> ();
	}
	
	// Update is called once per frame
	void Update ()
	{
		getHungry();
		UpdateBar ();
	}

	public void UpdateBar(){
		hungerBar.updateBar (food, maxFood);
	}

	void getHungry(){
		if (food > 0) {
			food -= 0.5f * Time.deltaTime;
		} else {
			health.takeDamage (Time.deltaTime);
		}
	}
}


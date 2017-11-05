using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasController : MonoBehaviour {

	public GameObject player;
	public GameObject panel;


	private Player playerController;
	private bool visible;

	void Start () {
		visible = false;
		playerController = player.gameObject.GetComponent<Player> ();
	}

	void Update () {
		checkGameOver ();
	}

	void checkGameOver (){
		if (playerController.isDead () && !visible) {
			visible = true;
			StartCoroutine(waitAndGameOver(playerController.timeOfDead));
		}
	}

	IEnumerator waitAndGameOver(float waitTime)
	{
		yield return new WaitForSecondsRealtime(waitTime);
		panel.SetActive (true);
	}
}

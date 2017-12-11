using UnityEngine;
using System.Collections;

public class VisibilityToggle : MonoBehaviour {

	public KeyCode key;
	public GameObject gameObjectB;

	// Use this for initialization
	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update () {
		checkActiveOrUnactive ();
	}

	public void checkActiveOrUnactive (){
		if (Input.GetKeyDown (key)) {
			ToggleVisibility ();
		}
	}

	public void ToggleVisibility(){
		gameObjectB.SetActive (!gameObjectB.activeSelf);
	}
}


using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealthBar : StatBar
{
	private Color color;
	private Image image;

	void Start(){
		image = bar.GetComponent<Image> ();
		this.color = image.color;
	}

	override public void updateBar(float value, float maxValue){
		if (value < 0.5 * maxValue) {
			image.color = Color.red;
		}

		float normalizedValue = normalizeValue (value, maxValue);

		image.transform.localScale = new Vector3(barScale.x * normalizedValue * 0.01f, scale.y, scale.x);
	}

	public void revive(){
		image.color = color;
	} 
}


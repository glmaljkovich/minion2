using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterHealthBar : StatBar
{

	override public void updateBar(float value, float maxValue){
		SpriteRenderer image = bar.GetComponent<SpriteRenderer> ();

		float normalizedValue = normalizeValue (value, maxValue);

		image.color = Color.Lerp(image.material.color, Color.red, 1 - normalizedValue * 0.01f);
		image.transform.localScale = new Vector3(barScale.x * normalizedValue * 0.01f, scale.y, scale.x);
	}
}


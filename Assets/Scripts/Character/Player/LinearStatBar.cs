using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class LinearStatBar : StatBar
{

	override public void updateBar(float value, float maxValue){
		float normalizedValue = normalizeValue (value, maxValue);
		Image image = bar.GetComponent<Image> ();
		image.transform.localScale = new Vector3(barScale.x * normalizedValue * 0.01f, scale.y, scale.x);
	}
}


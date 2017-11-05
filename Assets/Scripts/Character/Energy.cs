using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energy : MonoBehaviour {

	public float totalEnergy;
	public float actualEnergy;

	public Energy(){
		actualEnergy = totalEnergy;
	}

	public bool canUse (float skillCost){
		if (actualEnergy >= skillCost) {
			actualEnergy = actualEnergy - skillCost;
			return true;
		}
		return false;
	}	
}

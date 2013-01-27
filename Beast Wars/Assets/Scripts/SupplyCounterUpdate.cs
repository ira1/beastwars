using UnityEngine;
using System.Collections;

public class SupplyCounterUpdate : MonoBehaviour {
	public int supplyAvailable = 6;
	float lastTickTime;
	
	// Use this for initialization
	void Start () {
		guiText.text = supplyAvailable.ToString();
		lastTickTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (Time.time - lastTickTime >= 1) {
			supplyAvailable ++;
			lastTickTime = Time.time;
			guiText.text = supplyAvailable.ToString();

		};
	
	}
}

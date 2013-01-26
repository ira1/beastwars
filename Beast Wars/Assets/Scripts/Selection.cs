using UnityEngine;
using System.Collections;

public class Selection : MonoBehaviour {
	public bool Selected;
	public Transform Halo;
	
	
	// Update is called once per frame
	void Update () {
	
		if (Halo)
		{
			Halo.gameObject.SetActive(Selected);
		}
	}
}

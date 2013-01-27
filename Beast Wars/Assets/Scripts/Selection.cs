using UnityEngine;
using System.Collections;

public class Selection : MonoBehaviour {
	public bool Selected;
	public Transform Halo;
	public int SelectionKey = 1;
	
	
	// Update is called once per frame
	void Update () {
	
		if (Halo)
		{
			Halo.gameObject.SetActive(Selected);
		}
	}
}

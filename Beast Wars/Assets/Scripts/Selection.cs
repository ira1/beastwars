using UnityEngine;
using System.Collections;

public class Selection : MonoBehaviour {
	public bool Selected;
	public Transform Halo;
	
	
	// Use this for initialization
	void Start () {
		
	
	}
	
	// Update is called once per frame
	void Update () {
	
		if (Halo)
		{
			Halo.gameObject.SetActive(Selected);
		}
	}
}

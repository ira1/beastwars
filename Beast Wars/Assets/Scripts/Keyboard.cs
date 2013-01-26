using UnityEngine;
using System.Collections.Generic;


public class Keyboard : MonoBehaviour {
	
	List<string> SelectedTags = new List<string>();
	
	
	// Use this for initialization
	void Start () {
	
	}

	void ToggleSelect (string tag)
	{
		if (SelectedTags.Contains(tag))
			SelectedTags.Remove(tag);
		else
			SelectedTags.Add(tag);
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			ToggleSelect("Player Toad");
		}
	
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			ToggleSelect("Player Rabbit");
		}

		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			ToggleSelect("Player Kangaroo");
		}

		UpdateSelections();
		
	}

	void UpdateSelections ()
	{
		var selection_objects = GameObject.FindObjectsOfType(typeof(Selection));
		foreach( Selection selection in selection_objects)
		{
			selection.Selected = SelectedTags.Contains( selection.gameObject.tag );
		}
	}

	void SelectAll (string tag)
	{
		Debug.Log("Selected " + tag);
		
		GameObject[] animals=GameObject.FindGameObjectsWithTag(tag);
		foreach(GameObject animal in animals)
		{
			animal.GetComponent<Selection>();
			Selection selection=animal.GetComponent<Selection>();
			selection.Selected=true;
		}
	}
}
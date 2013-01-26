using UnityEngine;
using System.Collections.Generic;


public class Keyboard : MonoBehaviour {
	
	List<string> SelectedTags = new List<string>();
	public Transform QPrefab;
	public Transform QLocation;
	
	public Transform WPrefab;
	public Transform WLocation;

	public Transform EPrefab;
	public Transform ELocation;

	public Vector3 RandomizeStarLocation = new Vector3(10,10,10);
	
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
		
		if (Input.GetKeyDown(KeyCode.Q))
		{
			CreateAnimal(QPrefab, QLocation);
		}
		
		if (Input.GetKeyDown(KeyCode.W))
		{
			CreateAnimal(WPrefab, WLocation);
		}

		if (Input.GetKeyDown(KeyCode.E))
		{
			CreateAnimal(EPrefab, ELocation);
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
	
	
	void CreateAnimal (Transform prefab, Transform location)
	{
		if (!prefab || !location)
			return;
		
		var animal = Instantiate(prefab) as Transform;
		
		Vector3 offset;
		offset.x = Random.Range(0, RandomizeStarLocation.x);
		offset.y = Random.Range(0, RandomizeStarLocation.y);
		offset.z = Random.Range(0, RandomizeStarLocation.z);
		
		
		animal.position = location.position + offset;
	}
}
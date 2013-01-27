using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AIOverlord : MonoBehaviour {
	
	float LevelStartTime;
	public List<Wave> Waves = new List<Wave>();
	
	void Start()
	{
		LevelStartTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () 
	{
		foreach(var wave in Waves)
		{
			if (wave.Done)
				continue;
			
			if (Time.time > wave.TimeStamp)
			{
				DoWave(wave);
			}
		}
	}
	
	public void DoWave(Wave wave)
	{
		wave.Done = true;
		foreach(AnimalSpec spec in wave.Animals)
		{
			SpawnAnimals(spec);
		}
		
	}
	
	

	public void SpawnAnimals (AnimalSpec spec)
	{
		for (int i = 0; i < spec.NumAnimals; ++i)
		{
			
			Vector3 location = PickPointInsideBounds( spec.SpawnLocation );
			var animal = Instantiate(spec.AnimalPrefab) as Transform;
			animal.position = location;
		}
	}
	
	
	[Serializable]
	public class Wave
	{
		public List<AnimalSpec> Animals = new List<AnimalSpec> ();
		public float TimeStamp;
		public bool Done = false;
	}
	
	[Serializable]
	public class AnimalSpec
	{
		public Transform AnimalPrefab;
		public GameObject SpawnLocation;
		public int NumAnimals =1;
	}
	
	
	
	public static Vector3 PickPointInsideBounds(GameObject gameobj)
	{
		Bounds bounds = gameobj.collider.bounds;
		
		Vector3 point = new Vector3(
			UnityEngine. Random.Range(bounds.min.x, bounds.max.x),
			UnityEngine. Random.Range(bounds.min.y, bounds.max.y),
			UnityEngine. Random.Range(bounds.min.z, bounds.max.z)
			);
		
		return point;
	}
}

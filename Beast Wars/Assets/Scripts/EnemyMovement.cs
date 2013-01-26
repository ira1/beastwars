using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemyMovement : AnimalMovement {
	
	public Transform Warren;
	public float SeeEnemyDistance = 100;
	public float AttackEnemyDistance = 10;
	public float DamagePerSecond = 50;
	
	// Charge towards player warrens
	// If you see a player animal, move toward it
	// If you are close to a player animal, attack it

	public override void Start ()
	{
		base.Start ();
	}
	
	public override void Update ()
	{
		if (MaybeAttack())
			return;
		if (MaybeMoveToPlayerAnimal())
			return;
		
		MoveToWarren();
	}
	
	public float DistanceToMeSquared(GameObject go)
	{
		Vector3 offset = this.transform.position - go.transform.position;
		return offset.sqrMagnitude;
	}

	public bool MaybeAttack ()
	{
		var objects = GameObject.FindObjectsOfType(typeof(Selection));
		float too_far = AttackEnemyDistance * AttackEnemyDistance;
		
		var selections = objects.Select( s => (Selection)s);
		
		
		foreach( Selection selection in selections.OrderBy( s =>  this.DistanceToMeSquared(s.gameObject) ))
		{
			Vector3 offset = this.transform.position - selection.transform.position;
			
			if (offset.sqrMagnitude > too_far)
				return false;

			Attack (selection.gameObject);
			return true;
		}
		
		
		return false;
	}

	public void Attack (GameObject gameObject)
	{
		gameObject.GetComponent<AnimalMovement>().Health -= DamagePerSecond * Time.deltaTime;
	}
	
	public bool MaybeMoveToPlayerAnimal ()
	{
		// Find the nearest animal.
		float closest_distance_sqr = float.MaxValue;
		GameObject target = null;
		
		var animals = GameObject.FindObjectsOfType(typeof(Selection));
		float too_far = SeeEnemyDistance * SeeEnemyDistance;
		
		
		foreach( Selection selection in animals)
		{
			Vector3 offset = transform.position - selection.transform.position;
			float d2 = offset.sqrMagnitude;
			if (d2 > too_far)
				continue;
			
			
			if (d2 < closest_distance_sqr)
			{
				closest_distance_sqr = d2;
				target = selection.gameObject;
			}
		}
		
		if (target)
		{
			destination = target.transform.position;
			base.Update();
			return true;
		}
		
		return false;
		
	}

	public void MoveToWarren ()
	{
		//  Do we have warren picked out?
		if (!Warren)
		{
			// No!  Pick a warren.
			var possible_warrens = GameObject.FindGameObjectsWithTag("Player Warren");
			if (possible_warrens.Length < 1)
				return;
			
			Warren = possible_warrens[ Random.Range(0, possible_warrens.Length) ].transform;
			
		}
		
		destination = Warren.position;
		base.Update();
	}

	
	override public bool IsSelected ()
	{
		return false;
	}
	
	public override void onclick ()
	{
		// do nothing.
	}
}
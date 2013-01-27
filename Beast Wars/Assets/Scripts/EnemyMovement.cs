using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemyMovement : AnimalMovement {
	
	public Transform Warren;
	
	// Charge towards player warrens
	// If you see a player animal, move toward it
	// If you are close to a player animal, attack it

	public override void Start ()
	{
		base.Start ();
	}
	
	public override void Update ()
	{
		MaybeDoStuff();
	}

	public override bool MaybeDoStuff ()
	{
		if (!base.MaybeDoStuff ())
			MoveToWarren();
		
		return true;
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
		
		MoveTo(Warren.position);
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
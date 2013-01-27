using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class EnemyMovement : AnimalMovement {

	public WarrenHealth Warren;
	
	public float WarrenAttackDistance = 10;
		
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
		float toofar = WarrenAttackDistance*WarrenAttackDistance;
		if (!base.MaybeDoStuff ())
			MoveToWarren();
		if (Warren)
		{
			var offset = Warren.transform.position - transform.position;
			if(offset.sqrMagnitude < toofar)
			{
				AttackWarren();
			}
		}
		return true;
	}

	public void AttackWarren ()
	{
		Warren.Health = Warren.Health - DamagePerSecond*Time.deltaTime;
	}


	public void MoveToWarren ()
	{
		//  Do we have warren picked out?
		if (!Warren)
		{
			// No!  Pick a warren.
			var possible_warrens = GameObject.FindObjectsOfType(typeof(WarrenHealth));
			if (possible_warrens.Length < 1)
				return;

			Warren = possible_warrens[ Random.Range(0, possible_warrens.Length) ] as WarrenHealth;

		}

		MoveTo(Warren.transform.position);
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
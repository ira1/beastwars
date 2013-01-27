using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class AnimalMovement : MonoBehaviour {
	public string EnemyTag = "Enemy";


	public Texture jumping;
	public Texture ground;
	public float timetoswap;
	public float switchtime=1;
	public Vector3 MoveCommandDestination=new Vector3();
	public bool HasMoveCommand = false;
	public float speed=100;
	public float FeetOffGroundY = 7;
	public float Health = 100;
	public Transform CorpsePrefab;
	public float SeeEnemyDistance = 100;
	public float AttackEnemyDistance = 10;
	public float DamagePerSecond = 50;


	// Use this for initialization
	virtual public void Start () {
		MoveCommandDestination=transform.position;
	}

	void SwapTextures ()
	{
	if(timetoswap<Time.time){
	if(this.renderer.material.mainTexture==jumping)
		this.renderer.material.mainTexture=ground;
	else
		this.renderer.material.mainTexture=jumping;
	timetoswap=Time.time+switchtime;
		}
	}

	// Update is called once per frame
	public virtual void Update () {

		MaybeDoStuff ();

	}

	virtual public bool MaybeDoStuff ()
	{
		if (Health <=0)
		{
			if(CorpsePrefab)
			{
				var corpse = Instantiate(CorpsePrefab) as Transform;
				corpse.position = transform.position;
			}
			Destroy(this.gameObject);
			return true;
		}

		if (MaybeAttack())
			return true;
		if (MaybeMoveToPlayerAnimal())
			return true;

		if(Input.GetMouseButtonDown(0))
			onclick();

		if (HasMoveCommand)
		{
			MoveTo (MoveCommandDestination);
			return true;
		}

		return false;
	}

	public void MoveTo(Vector3 destination)
	{
		Vector3 to_destination=destination-transform.position;
		to_destination.y=0;
		Vector3 direction=to_destination;
		direction.Normalize();
		Vector3 velocity=direction*speed;

		Vector3 velocity_without_y = rigidbody.velocity;
		velocity_without_y.y = 0;

		Debug.Log(velocity);

		rigidbody.AddForce(-velocity_without_y, ForceMode.VelocityChange);
		if(to_destination.magnitude>Time.deltaTime*speed && transform.position.y < FeetOffGroundY)
		{
			rigidbody.AddForce(velocity, ForceMode.VelocityChange);
		}
		SwapTextures();
	}

	virtual public bool IsSelected ()
	{
		Selection selection = GetComponent<Selection>();
		if (selection && selection.Selected)
			return true;
		else
			return false;
	}

	virtual public void onclick()
	{
		if (!IsSelected())
			return;

		Camera camera=Camera.mainCamera;
		print(Input.mousePosition);
		//var mousePo = camera.ScreenToWorldPoint (new Vector3 (Input.mousePosition.x, Input.mousePosition.y,camera.nearClipPlane));
		var ray = camera.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;
		if(Physics.Raycast(ray.origin,ray.direction, out hit)){
			//print("world point on terrain: "+hit.point+", distance to point: "+hit.distance);
			MoveCommandDestination=hit.point;
			HasMoveCommand = true;
		}
	}	



	public float DistanceToMeSquared(GameObject go)
	{
		Vector3 offset = this.transform.position - go.transform.position;
		return offset.sqrMagnitude;
	}

	public bool MaybeAttack ()
	{
		var objects = GameObject.FindGameObjectsWithTag(EnemyTag);
		float too_far = AttackEnemyDistance * AttackEnemyDistance;



		foreach( GameObject obj in objects.OrderBy( go =>  this.DistanceToMeSquared(go) ))
		{
			Vector3 offset = this.transform.position - obj.transform.position;

			if (offset.sqrMagnitude > too_far)
				return false;

			Attack (obj);
			return true;
		}


		return false;
	}

	public void Attack (GameObject gameObject)
	{
		gameObject.GetComponent<AnimalMovement>().Health -= DamagePerSecond * Time.deltaTime;
		Debug.Log(this.gameObject.name +   " Munches " + gameObject.name);
	}

	public bool MaybeMoveToPlayerAnimal ()
	{
		var objects = GameObject.FindGameObjectsWithTag(EnemyTag);
		float too_far = SeeEnemyDistance * SeeEnemyDistance;

		foreach( GameObject obj in objects.OrderBy( go =>  this.DistanceToMeSquared(go) ))
		{
			Vector3 offset = this.transform.position - obj.transform.position;

			if (offset.sqrMagnitude > too_far)
				return false;

			MoveTo(obj.transform.position);
			Debug.Log(gameObject.name + " charging toward " + obj.gameObject.name);
			return true;
		}


		return false;
	}		


}

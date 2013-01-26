using UnityEngine;
using System.Collections;

public class AnimalMovement : MonoBehaviour {
	public Texture jumping;
	public Texture ground;
	public float timetoswap;
	public float switchtime=1;
	public Vector3 destination=new Vector3();
	public float speed=100;
	public float FeetOffGroundY = 7;
	public float Health = 100;
	
	// Use this for initialization
	virtual public void Start () {
		destination=transform.position;
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
		
		if (Health <=0)
		{
			Destroy(this.gameObject);
			return;
		}
		
		
	if(Input.GetMouseButtonDown(0))
		onclick();
	Vector3 to_destination=destination-transform.position;
		to_destination.y=0;
		Vector3 direction=to_destination;
		direction.Normalize();
	Vector3 velocity=direction*speed;
		
		Vector3 velocity_without_y = rigidbody.velocity;
		velocity_without_y.y = 0;
		
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
		print("world point on terrain: "+hit.point+", distance to point: "+hit.distance);
		destination=hit.point;
		}
	}	
}
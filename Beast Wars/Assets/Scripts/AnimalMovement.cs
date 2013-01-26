using UnityEngine;
using System.Collections;

public class AnimalMovement : MonoBehaviour {
	public Vector3 destination=new Vector3();
	public float speed=100;
	// Use this for initialization
	void Start () {
	destination=transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	if(Input.GetMouseButtonDown(0))
		onclick();
	Vector3 direction=destination-transform.position;
		direction.Normalize();
	Vector3 velocity=direction*speed;
		
		rigidbody.AddForce(-rigidbody.velocity, ForceMode.VelocityChange);
		rigidbody.AddForce(velocity, ForceMode.VelocityChange);
	}
public void onclick(){
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
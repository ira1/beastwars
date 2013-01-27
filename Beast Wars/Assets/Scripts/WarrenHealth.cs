using UnityEngine;
using System.Collections;

public class WarrenHealth : MonoBehaviour {
	public float Health = 1000;
	public Transform CorpsePrefab;
	
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	if (Health <=0)
		{
			if(CorpsePrefab)
			{
				var corpse = Instantiate(CorpsePrefab) as Transform;
				corpse.position = transform.position;
			}
			Destroy(this.gameObject);
		}
	}
}

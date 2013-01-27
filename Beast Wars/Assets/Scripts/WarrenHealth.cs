using UnityEngine;
using System.Collections;

public class WarrenHealth : MonoBehaviour {
	public float Health = 1000;
	public Transform CorpsePrefab;
	public Texture glowing;
	public Texture dull;
	public float timetoswap;
	public float switchtime=3;
	
	// Use this for initialization
	void Start () {
	}
	void SwapTextures ()
	{
	if(timetoswap<Time.time){
	if(this.renderer.material.mainTexture==glowing)
		this.renderer.material.mainTexture=dull;
	else
		this.renderer.material.mainTexture=glowing;
	timetoswap=Time.time+switchtime;
		}
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
	SwapTextures();	
	}
}

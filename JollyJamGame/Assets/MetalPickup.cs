using UnityEngine;
using System.Collections;

public class MetalPickup : MonoBehaviour {

	private bool _collected = false;
	public int HitPoints = 1;

	public void SetCollected(bool col){
		_collected = col;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnCollisionEnter2D(Collision2D c)
	{
		if(_collected){
			//Picking up more metal
			if(c.gameObject.layer == LayerMask.NameToLayer("Pickups")){
				c.transform.parent = transform;
			}
			//Getting hit by a bullet
			if(c.gameObject.layer == LayerMask.NameToLayer("Bullets")){
				if(--HitPoints == 0){
					Detach();
					_collected = false;
				}
			}
		}

	}

	//Handles detaching this metal and child metals too
	public void Detach(){
		transform.parent = null;
	}

}


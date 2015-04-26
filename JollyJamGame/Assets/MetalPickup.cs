using UnityEngine;
using System.Collections;

public class MetalPickup : MonoBehaviour {
	//How deep in the parent tree are we
	public int Depth = 0;

	private bool _collected = false;

	//Am I currently collectable by player?
	public bool _collectable = true;
	private Timer _collect_timer;
	public float TimeToRecollect;



	//My hit points
	public int HitPoints = 1;

	public void SetCollected(bool col){
		_collected = col;
	}

	// Use this for initialization
	void Start () {
		_collect_timer = new Timer();
	}
	
	// Update is called once per frame
	void Update () {
		if(!_collectable && _collect_timer.isDone){
			_collectable = true;
		}
	}

	//Picking up metal
	void OnCollisionEnter2D(Collision2D c) {
		if(_collected){
			//Picking up more metal
			if(c.gameObject.layer == LayerMask.NameToLayer("Pickups")){
				c.transform.parent.parent = this.transform;
				c.gameObject.layer = LayerMask.NameToLayer("PlayerHeld");
			}
		}
	}

	//Handles detaching this metal and child metals too
	public float Detach(){
		transform.parent = null;

		_collectable = false;
		_collect_timer.SetTimer(TimeToRecollect);

		Destroy(gameObject); //We just die right now

		//Kill children
		float subtreemass = 0.0f;
		foreach(Transform child in transform){
			GameObject go = child.gameObject;
			if(go.tag == "Metal"){
				subtreemass += go.GetComponent<MetalPickup>().Detach();
			}
		}
		return subtreemass;
	}

}


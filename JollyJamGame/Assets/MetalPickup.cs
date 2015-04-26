using UnityEngine;
using System.Collections;

public class MetalPickup : MonoBehaviour {
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

	//Bullets
	/*
	void OnTriggerEnter2D(Collider2D c){
		if(_collected){
			//Getting hit by a bullet
			if(c.gameObject.layer == LayerMask.NameToLayer("Bullets")){
				if(--HitPoints == 0){
					Detach();
					_collected = false;
				}
			}
		}
	}
	*/

	//Picking up metal
	void OnCollisionEnter2D(Collision2D c) {
		if(_collected){
			//Picking up more metal
			if(c.gameObject.layer == LayerMask.NameToLayer("Pickups")){
				c.transform.parent.parent = this.transform; //Wow lol
				c.gameObject.layer = LayerMask.NameToLayer("PlayerHeld");
			}
		}
	}

	//Handles detaching this metal and child metals too
	public void Detach(){
		transform.parent = null;
		Destroy(gameObject); //We just die right now
		_collectable = false;
		_collect_timer.SetTimer(TimeToRecollect);
	}

}


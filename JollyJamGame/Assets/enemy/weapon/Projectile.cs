using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	private Timer _timer;
	public float TimeToDie = 10f;

	void Start(){
		_timer = new Timer();
		_timer.SetTimer(TimeToDie);
	}

	void Update(){
		if(_timer.isDone){
			Object.Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D c){
		GameObject.Destroy (GetComponent<Rigidbody2D> ());
		Destroy (gameObject, 0.5f);
	}
}


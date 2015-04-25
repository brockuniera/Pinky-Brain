using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	private SoundManager soundManager;

	private Timer _timer;
	public float TimeToDie = 10f;

	void Start(){
		soundManager = GameObject.FindWithTag ("GameController").GetComponent<GameManager> ().soundManager;

		_timer = new Timer();
		_timer.SetTimer(TimeToDie);
	}

	void Update(){
		if(_timer.isDone){
			Object.Destroy(gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D c){
		if(c.gameObject.layer == LayerMask.NameToLayer("PlayerHeld")){
			Destroy(c.transform.parent.gameObject);
			soundManager.playSound("Boom");
		}
		GameObject.Destroy (GetComponent<Rigidbody2D> ());
		Destroy (gameObject, 0.5f);
	}
}


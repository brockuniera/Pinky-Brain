using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	private SoundManager soundManager;

	private Timer _timer;
	public float TimeToDie = 10f;
	public float ForceToAdd;
	public float ForceToAddPlayer;

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
			//Push against player
			Vector2 offset = (GetComponent<Rigidbody2D>().velocity).normalized;
			c.attachedRigidbody.AddForce(offset * ForceToAdd);

			//Knock/destroy metal or whatever
			c.transform.parent.gameObject.GetComponent<MetalPickup>().Detach();
			soundManager.playSound("Boom");
			soundManager.playSound("Ricochet2");
		}
		else if(c.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			soundManager.playSound("PlayerGetsHit");
			GameObject player = c.transform.parent.gameObject;
			if(player.GetComponent<move>().pickups == 0){
				//you died, idiot!
				Application.LoadLevel(Application.loadedLevel);
			}else{
				//Push against player
				Vector2 offset = (GetComponent<Rigidbody2D>().velocity).normalized;
				c.attachedRigidbody.AddForce(offset * ForceToAddPlayer);
				player.GetComponent<move>().DropMetal();
			}
		}
		GameObject.Destroy (GetComponent<Rigidbody2D> ());
		GameObject.Destroy (GetComponent<Collider2D> ());
		Destroy (gameObject, 0.5f);
	}
}


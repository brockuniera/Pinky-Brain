using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	private Timer _timer;
	public float TimeToDie = 10f;

	void Start(){
		_timer = new Timer();
		//_timer.SetTimer(TimeToDie);
		_timer.SetTimer(10f);
	}

	void Update(){
		print(_timer.getTime);
		if(_timer.isDone){
			print("DIEING!!!");
			Object.Destroy(gameObject);
		}
	}

	// boom go here
	public void onContact()
	{

	}
}

using UnityEngine;
using System.Collections;

public class MetalPickup : MonoBehaviour {

	private bool _collected = false;

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
			c.transform.parent = transform;
		}

	}

}


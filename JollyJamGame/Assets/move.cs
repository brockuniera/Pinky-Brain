using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	

		transform.Translate(Input.GetAxis("Horizontal")*Time.deltaTime * 2,Input.GetAxis("Vertical")*Time.deltaTime * 2,0);
	}
	void OnTriggerStay(Collider c)
	{
		c.transform.parent = this.transform;
	}
}

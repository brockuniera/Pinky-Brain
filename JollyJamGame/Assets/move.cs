using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Input.GetAxis("Horizontal")*Time.deltaTime * 2,Input.GetAxis("Vertical")*Time.deltaTime * 2,0);
		transform.Rotate(Vector3.right, Time.deltaTime);
		//transform.Rotate(Vector3.up, Time.deltaTime, Space.World);
	}
	void OnTriggerStay(Collider c)
	{
		c.transform.parent = this.transform;
	}
}

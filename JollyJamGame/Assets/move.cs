using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {
	private float xAxis_rotate;
	private float xAxis_move;
	private float yAxis_move;
	private Rigidbody2D rb2d;
	public float alt_move;
	private float goalSpeed;
	public float rot;
	private float maxSpeed_rot; 
	public float speed_max;
	public float speed_mult;
	public bool trans;

	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		//Read input values
		if (trans) {
			Application.LoadLevel(1);
		}
		xAxis_move = Input.GetAxis ("HorizontalLeft");
		yAxis_move = Input.GetAxis ("VerticalLeft");
		xAxis_rotate = Input.GetAxis ("HorizontalRight");
		rb2d.centerOfMass = Vector2.zero;
	}

	//Movement
	void FixedUpdate () {
		goalSpeed = speed_mult * rb2d.mass + speed_max;
		Vector2 force_added = new Vector2 (xAxis_move * alt_move, yAxis_move * alt_move * -1f);
		if (rb2d.velocity.sqrMagnitude < goalSpeed) {
			rb2d.AddForce(force_added);
			//rb2d.velocity = new Vector2(xAxis_move * alt_move, yAxis_move * alt_move * -1f);
		} 
		maxSpeed_rot = speed_max * rb2d.mass * rot;
		if (Mathf.Abs(rb2d.angularVelocity) < maxSpeed_rot) {			
			rb2d.AddTorque (xAxis_rotate * rot);
		}
	}

	//Picking up metal
	void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.layer == LayerMask.NameToLayer("Pickups")) {
			c.transform.parent = this.transform;
			c.gameObject.GetComponent<MetalPickup>().SetCollected(true);
			rb2d.mass += c.gameObject.GetComponent<Metal>().weight;
		}
	}
}


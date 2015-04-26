using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {
	//Input vars
	//
	private float xAxis_rotate;
	private float xAxis_move;
	private float yAxis_move;

	//Cache rb2d
	//
	private Rigidbody2D rb2d;

	//Movement multiplier
	//
	public float alt_move;

	//Rotation
	//
	public float RotationSpeed;

	//Acceleration
	public float speed_max;
	public float speed_mult;

	//TODO Add losing weight
	//public void 

	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}

	void Update () {
		//Read input values
		xAxis_move = Input.GetAxis ("HorizontalLeft");
		yAxis_move = Input.GetAxis ("VerticalLeft");
		xAxis_rotate = -Input.GetAxis ("HorizontalRight");

	}

	//Movement
	void FixedUpdate () {
		//Every frame, to ensure rotations are correct
		rb2d.centerOfMass = Vector2.zero;

		float goalSpeed = speed_mult * rb2d.mass + speed_max;
		Vector2 force_added = new Vector2 (xAxis_move * alt_move, yAxis_move * alt_move * -1f);
		if (rb2d.velocity.sqrMagnitude < goalSpeed) {
			rb2d.AddForce(force_added);
			//rb2d.velocity = new Vector2(xAxis_move * alt_move, yAxis_move * alt_move * -1f);
		} 
		if(xAxis_rotate != 0.0f){
			/*
			float rotMax = MaxRotationSpeedMult * rb2d.mass + MaxRotationSpeedBase;
			print(rotMax);
			*/

			rb2d.angularVelocity = xAxis_rotate * RotationSpeed;
		}
	}

	//Picking up metal
	void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.layer == LayerMask.NameToLayer("Pickups")) {
			//Parenting
			c.transform.parent.parent = this.transform;

			//Setup layers
			c.gameObject.layer = LayerMask.NameToLayer("PlayerHeld");
			//Enable child
			c.transform.parent.GetComponent<MetalPickup>().SetCollected(true);

			//get heavier
			rb2d.mass += c.gameObject.GetComponent<Metal>().weight;
		}
	}
}


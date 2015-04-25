using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {
	private SoundManager soundManager;

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
	public bool trans;

	private bool isWrappingX = false;
	private bool isWrappingY = false;
	private Renderer[] renderers;

	public int pickups {
		get { return numPickups (); }
	}

	//TODO Add losing weight
	//public void 

	void Start () {
		soundManager = GameObject.FindWithTag ("GameController").GetComponent<GameManager> ().soundManager;

		rb2d = GetComponent<Rigidbody2D> ();
		renderers = GetComponentsInChildren<Renderer>();
	}

	void Update () {
		//Read input values
		if (trans) {
			Application.LoadLevel(1);
		}

		xAxis_move = Input.GetAxis ("HorizontalLeft");
		yAxis_move = Input.GetAxis ("VerticalLeft");
		xAxis_rotate = -Input.GetAxis ("HorizontalRight");
		rb2d.centerOfMass = Vector2.zero;

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
			soundManager.playSound("Attach");
			c.transform.parent.parent = this.transform;

			//Setup layers
			c.gameObject.layer = LayerMask.NameToLayer("PlayerHeld");
			//Enable child
			c.transform.parent.GetComponent<MetalPickup>().SetCollected(true);

			//get heavier
			rb2d.mass += c.gameObject.GetComponent<Metal>().weight;
		}

	}

	public Transform tele_dest_low1;
	public Transform tele_dest_low2;
	public Transform tele_dest_low3;
	public Transform tele_dest_low4;

	void OnTriggerEnter2D(Collider2D c) {
		if (c.gameObject.layer == LayerMask.NameToLayer ("wrap")) {
			Vector3 newPos = transform.position;
			
			if (newPos.x > 1) {
				newPos.x = tele_dest_low3.position.x;
			}
			if (newPos.x < 0) {
				newPos.x = tele_dest_low1.position.x;
			}
			if (newPos.y > 1) {
				newPos.y = tele_dest_low2.position.y;
			}
			if (newPos.y < 0) {
				newPos.y = tele_dest_low4.position.y;
			}
			
			transform.position = newPos;
		}
	}

	private int numPickups()
	{
		int o = 0;
		foreach (Transform t in transform) {
			if(t.gameObject.layer == LayerMask.NameToLayer("Pickups")) o++;
		}
		
		return o;
	}
}


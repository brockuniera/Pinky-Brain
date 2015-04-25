using UnityEngine;
using System.Collections;

public class move : MonoBehaviour {
	private float xAxis_rotate;
	private float xAxis_move;
	private float yAxis_move;
	private Rigidbody2D rb2d;
	public float alt_move;
	public float goalSpeed;
	// Use this for initialization
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		xAxis_move = Input.GetAxis ("HorizontalLeft");
		yAxis_move = Input.GetAxis ("VerticalLeft");
		xAxis_rotate = Input.GetAxis ("HorizontalRight");

		rb2d.centerOfMass = Vector2.zero;
	}

	void FixedUpdate () {
		if (rb2d.velocity.sqrMagnitude < goalSpeed) {
			rb2d.velocity = new Vector2(xAxis_move * alt_move, yAxis_move * alt_move);
		}
		rb2d.AddTorque (xAxis_rotate);
	}

	void OnCollisionEnter2D(Collision2D c)
	{
		if (c.gameObject.layer == LayerMask.NameToLayer("Pickups")) {
			c.transform.parent = this.transform;
			c.gameObject.GetComponent<MetalPickup>().SetCollected(true);
		}
	}
}


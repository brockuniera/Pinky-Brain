using UnityEngine;
using System.Collections.Generic;

public class move : MonoBehaviour {
	//Death
	//
	public bool isDead {get; set;}

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
	public float MaxRotationSpeedMult;
	public float MaxRotationSpeedBase;

	//Acceleration
	public float speed_max;
	public float speed_mult;
	public bool trans;

	private bool isWrappingX = false;
	private bool isWrappingY = false;
	private Renderer[] renderers;

	private bool noArmorFlag;

	public int pickups {
		get { return numPickups (); }
	}

	//TODO Add losing weight
	//public void 

	void Awake()
	{
		noArmorFlag = false;
	}

	void Start () {
		soundManager = GameObject.FindWithTag ("GameController").GetComponent<GameManager> ().soundManager;

		rb2d = GetComponent<Rigidbody2D> ();
		renderers = GetComponentsInChildren<Renderer>();
	}

	void Update () {
		//For loading levels or something
		if (trans) {
			Application.LoadLevel("level1");
		}

		if (pickups <= 0 && !noArmorFlag) {
			soundManager.loopSound ("NoArmor");
			noArmorFlag = true;
		} else if(pickups > 0 && noArmorFlag) {
			soundManager.stopLoop("NoArmor");
			noArmorFlag = false;
		}
		
		//Read input values
		xAxis_move = Input.GetAxis ("HorizontalLeft");
		yAxis_move = Input.GetAxis ("VerticalLeft");
		xAxis_rotate = Input.GetAxis ("HorizontalRight");
		xAxis_rotate = -Input.GetAxis ("HorizontalRight");

		rb2d.centerOfMass = Vector2.zero;
	}

	//Movement
	void FixedUpdate(){
		//Every frame, to ensure rotations are correct
		rb2d.centerOfMass = Vector2.zero;

		float maxSpeed = speed_mult * rb2d.mass + speed_max;
		Vector2 forceDir = new Vector2(xAxis_move, yAxis_move * -1f);


		//Push harder when "braking"
		if(forceDir.x < 0 != rb2d.velocity.x < 0){
			forceDir.x *= 2;
		}
		if(forceDir.y < 0 != rb2d.velocity.y < 0){
			forceDir.y *= 2;
		}

		//Move player
		rb2d.AddForce(forceDir * alt_move);

		//Cap the speed
		rb2d.velocity = Vector2.ClampMagnitude(rb2d.velocity, maxSpeed);

		if(xAxis_rotate != 0.0f){
			float rotSpeed = MaxRotationSpeedMult * rb2d.mass + MaxRotationSpeedBase;

			rb2d.angularVelocity = xAxis_rotate * rotSpeed;
		}
	}

	//Drop all held metal
	public void DropMetal(){
		List<MetalPickup> toDetach = new List<MetalPickup>();
		foreach(Transform c in transform){
			if(c.gameObject.layer == LayerMask.NameToLayer("PlayerHeld")){
				toDetach.Add(c.gameObject.GetComponent<MetalPickup>());
			}
		}
		foreach(MetalPickup mp in toDetach){
			mp.Detach();
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
			c.transform.parent.gameObject.layer = LayerMask.NameToLayer("PlayerHeld");
			c.gameObject.layer = LayerMask.NameToLayer("PlayerHeld");
			//Enable child
			c.transform.parent.GetComponent<MetalPickup>().SetCollected(true);

			//get heavier
			rb2d.mass += c.transform.parent.GetComponent<Metal>().weight;
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
			if(t.gameObject.layer == LayerMask.NameToLayer("PlayerHeld"))
				o++;
		}
		
		return o;
	}
}


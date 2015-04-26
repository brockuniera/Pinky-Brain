using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour {

	[SerializeField]
	[Range(0.0f, 10.0f)]
	private float difficulty;

	[SerializeField]
	private Transform player;

	public Transform barrel;

	[SerializeField]
	private float acceleration;

	[SerializeField]
	private Projectile projectile;

	private Rigidbody2D rigidbody2D;

	private bool touchingRock;
	private bool touchingPlayer;

	/*
	[SerializeField]
	private MovementType movementType;

	[SerializeField]
	private GunType gunType;

	public enum MovementType { NULL, STRAFE, TACKLE, ELLIPTICAL };
	public enum GunType { NULL, GUN, SHOTGUN };
	*/

	public IEnemyMovement movement;
	public IWeapon weapon;

	void Awake() {
		touchingPlayer = touchingRock = false;

		rigidbody2D = GetComponent<Rigidbody2D> ();
		player = GameObject.FindWithTag("Player").transform;

		transform.eulerAngles = new Vector3 (0.0f, 0.0f, 270.0f);

		generate (GameManager.Difficulty);

		/*
		switch (movementType) {
		case MovementType.STRAFE:
			movement = new StrafeMovement(this, player.transform, 10.0f, acceleration);
			break;
		
		case MovementType.TACKLE:
			movement = new StrafeMovement(this, player.transform, 10.0f, acceleration);
			break;

		case MovementType.ELLIPTICAL:
			movement = new EllipticalMovement (this, player.transform, acceleration, Random.Range(3.0f, 10.0f),
			                                   Random.Range(3.0f, 10.0f));
			break;
			
		default: case MovementType.NULL:
			break;
		}

		switch (gunType) {
		case GunType.GUN:
			weapon = new Gun(this, player.transform, projectile, 0.5f, 20.0f);
			break;

		case GunType.SHOTGUN:
			weapon = new Shotgun(this, player.transform, projectile, 0.5f, 20.0f, 20.0f, 3);
			break;

		default: case GunType.NULL:
			break;
		}
		*/
	}

	private void generate(float difficultyFactor)
	{
		float BaseFireRate = .5f;
		float BaseSpeed = 6.0f;

		if (Random.Range (0, 2) == 0)
			weapon = new Gun (this, player.transform, projectile, BaseFireRate * difficultyFactor / 3.0f, 10.0f * difficultyFactor);
		else
			weapon = new Shotgun (this, player.transform, projectile, BaseFireRate * difficultyFactor / 6.0f,
			                            10.0f * difficultyFactor, 20.0f, (int)(difficultyFactor * 3.0f));
		
		if (Random.Range (0, 2) == 0)
			movement = new StrafeMovement (this, player.transform, Random.Range (15.0f, 40.0f), BaseSpeed + (difficultyFactor * 2.0f));
		else
			movement = new EllipticalMovement (this, player.transform, BaseSpeed + (difficultyFactor * 2.0f), Random.Range (15.0f, 40.0f),
			                                         Random.Range (15.0f, 40.0f));
	}

	public void Update()
	{
		weapon.aim (Time.deltaTime);
		weapon.fire ();
	}

	void FixedUpdate() {
		// TODO this rotates on the wrong axis

		Vector2 to = player.transform.position - transform.position;
		transform.rotation = Quaternion.LookRotation (new Vector3 (to.x, to.y, transform.position.z), Vector3.forward);
		movement.move (Time.fixedDeltaTime);
	}

	void OnCollisionEnter2D(Collision2D c){
		if (c.gameObject.layer == LayerMask.NameToLayer ("Rock")) {
			touchingRock = true;
		} else if (c.gameObject.layer == LayerMask.NameToLayer ("Player")) {
			touchingPlayer = true;
		}

		if (touchingPlayer && touchingRock)
			Destroy (gameObject);
	}

	void OnCollisionExit2D(Collision2D c)
	{
		if (c.gameObject.layer == LayerMask.NameToLayer ("Rock")) {
			touchingRock = false;
		} else if (c.gameObject.layer == LayerMask.NameToLayer ("Player")) {
			touchingPlayer = false;
		}
	}
}

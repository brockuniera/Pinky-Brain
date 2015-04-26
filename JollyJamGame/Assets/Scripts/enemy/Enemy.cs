using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour {

	[SerializeField]
	private Transform player;

	public Transform barrel;

	[SerializeField]
	private float acceleration;

	[SerializeField]
	private Projectile projectile;

	private Rigidbody2D rigidbody2D;

	[SerializeField]
	private MovementType movementType;

	[SerializeField]
	private GunType gunType;

	public enum MovementType { NULL, STRAFE, TACKLE, ELLIPTICAL };
	public enum GunType { NULL, GUN, SHOTGUN };

	public IEnemyMovement movement;
	public IWeapon weapon;

	void Awake() {
		rigidbody2D = GetComponent<Rigidbody2D> ();
		player = GameObject.FindWithTag("Player").transform;

		transform.eulerAngles = new Vector3 (0.0f, 0.0f, 270.0f);

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

}

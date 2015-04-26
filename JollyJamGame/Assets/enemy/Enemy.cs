using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour {


	[SerializeField]
	private float BaseSpeed = 10.0f;
	[SerializeField]
	private float BaseFireRate = 2.0f;

	[SerializeField]
	private Transform player;

	[SerializeField]
	private float acceleration;

	[SerializeField]
	private Projectile projectile;

	private Rigidbody2D rigidbody2D;

	[SerializeField]
	private MovementType movementType;

	[SerializeField]
	private GunType gunType;

	public enum MovementType { NULL, STRAFE, TACKLE };
	public enum GunType { NULL, GUN, SHOTGUN };

	//
	//Mutators
	//

	void SetBaseSpeed(float bs){ BaseSpeed = bs; }
	void SetBaseFireRate(float bs){ BaseFireRate = bs; }
	
	public IEnemyMovement movement;
	public IWeapon weapon;

	void Awake() {
		rigidbody2D = GetComponent<Rigidbody2D> ();

		switch (movementType) {
		case MovementType.STRAFE:
			movement = new StrafeMovement(this, player.transform, 10.0f, acceleration);
			break;
		
		case MovementType.TACKLE:
			movement = new TackleMovement(this, player.transform, acceleration);
			break;

		default: case MovementType.NULL:
			break;
		}

		switch (gunType) {
		case GunType.GUN:
			weapon = new Gun(this, player.transform, projectile, 2.0f, 20.0f);
			break;

		case GunType.SHOTGUN:
			weapon = new Shotgun(this, player.transform, projectile, 2.0f, 20.0f, 20.0f, 3);
			break;

		default: case GunType.NULL:
			break;
		}
	}

	public void Update()
	{
		weapon.aim (Time.deltaTime);

		Projectile[] projectiles = weapon.fire ();
		if (projectiles != null) {
			for(int i = 0; i < projectiles.Length; i++)
			{
				// anything
			}
		}
	}

	void FixedUpdate() {
		movement.move (Time.fixedDeltaTime);
	}

}

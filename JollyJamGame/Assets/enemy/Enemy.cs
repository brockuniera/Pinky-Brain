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

	//
	//Mutators
	//

	void SetBaseSpeed(float bs){ BaseSpeed = bs; }
	void SetBaseFireRate(float bs){ BaseFireRate = bs; }
	
	public IEnemyMovement movement;
	public IWeapon weapon;

	void Awake() {
		rigidbody2D = GetComponent<Rigidbody2D> ();
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

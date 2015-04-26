using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour {

	[SerializeField]
	private Transform player;

	[SerializeField]
	private float acceleration;

	[SerializeField]
	private Projectile projectile;

	private Rigidbody2D rigidbody2D;
	
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

	public static Enemy Generate(Transform target, float difficultyFactor)
	{
		Projectile bullet = (Instantiate((Resources.Load("placeholders/bullet") as GameObject))).GetComponent<Projectile>();
		Enemy enemy = (Instantiate((Resources.Load("placeholders/enemy") as GameObject))).GetComponent<Enemy>();

		if (Random.Range (0, 2) == 0)
			enemy.weapon = new Gun (enemy, target, bullet, 2.0f - (difficultyFactor * 0.1f), 10.0f * difficultyFactor);
		else 
			enemy.weapon = new Shotgun (enemy, target, bullet, 4.0f - (difficultyFactor * 0.2f), 10.0f * difficultyFactor,
			                           20.0f, (int)(difficultyFactor * 3.0f));

		if (Random.Range (0, 2) == 0)
			enemy.movement = new StrafeMovement(enemy, target, Random.Range(5.0f, 10.0f), 20.0f);
		else 
			enemy.movement = new TackleMovement(enemy, target, 10.0f + (difficultyFactor * 2.0f));

		return null;
	}
}

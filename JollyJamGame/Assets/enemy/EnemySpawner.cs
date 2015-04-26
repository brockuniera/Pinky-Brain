using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	[Range(0.00f, 10.0f)]
	public float BaseSpeed = 2.0f;

	[Range(0.00f, 10.0f)]
	public float BaseFireRate = 0.5f;

	public Enemy Generate(Transform target, float difficultyFactor)
	{
		Projectile bullet = (Instantiate((Resources.Load("placeholders/bullet") as GameObject))).GetComponent<Projectile>();
		Enemy enemy = (Instantiate((Resources.Load("placeholders/enemy") as GameObject))).GetComponent<Enemy>();

		if (Random.Range (0, 2) == 0)
			enemy.weapon = new Gun (enemy, target, bullet, BaseFireRate + (difficultyFactor * 0.1f), 10.0f * difficultyFactor);
		else 
			enemy.weapon = new Shotgun (enemy, target, bullet, (BaseFireRate / 2.0f) + (difficultyFactor * 0.2f),
					10.0f * difficultyFactor, 20.0f, (int)(difficultyFactor * 3.0f));

			enemy.movement = new StrafeMovement(enemy, target, Random.Range(5.0f, 10.0f), BaseSpeed + (difficultyFactor * 2.0f));

		return null;
	}

}

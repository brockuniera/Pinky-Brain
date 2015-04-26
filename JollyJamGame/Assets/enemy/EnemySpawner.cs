using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

	public float BaseSpeed = 10.0f;
	public float TackleSpeedMultiplier = .1f;
	public float BaseFireRate = 2.0f;

	public Enemy Generate(Transform target, float difficultyFactor)
	{
		Projectile bullet = (Instantiate((Resources.Load("placeholders/bullet") as GameObject))).GetComponent<Projectile>();
		Enemy enemy = (Instantiate((Resources.Load("placeholders/enemy") as GameObject))).GetComponent<Enemy>();

		if (Random.Range (0, 2) == 0)
			enemy.weapon = new Gun (enemy, target, bullet, BaseFireRate + (difficultyFactor * 0.1f), 10.0f * difficultyFactor);
		else 
			enemy.weapon = new Shotgun (enemy, target, bullet, (2.0f * BaseFireRate) + (difficultyFactor * 0.2f),
					10.0f * difficultyFactor, 20.0f, (int)(difficultyFactor * 3.0f));

		if (Random.Range (0, 2) == 0)
			enemy.movement = new StrafeMovement(enemy, target, Random.Range(5.0f, 10.0f), BaseSpeed + (difficultyFactor * 2.0f));
		else 
			enemy.movement = new TackleMovement(enemy, target, BaseSpeed + (difficultyFactor * 2.0f) * TackleSpeedMultiplier);

		Debug.Log (enemy.movement);

		return null;
	}

}

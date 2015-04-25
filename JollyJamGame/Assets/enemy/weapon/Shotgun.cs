using System;
using UnityEngine;

public class Shotgun : Gun
{
	private float spread;
	private int amount;

	public Shotgun(Enemy enemy, Transform target, Projectile projectile, float fireRate, float velocity, float spread, int amount)
		: base(enemy, target, projectile, fireRate, velocity)
	{
		this.spread = spread;
		this.amount = amount;
	}

	public override Projectile[] fire()
	{
		if (fireTimer <= fireRate) {
			return null;
		} else {
			fireTimer = 0.0f;

			for(int i = 0; i < amount; i++)
			{
				Vector2 t = target.position - enemy.transform.position;
				float angle = ((Mathf.Atan2(t.y, t.x) * Mathf.Rad2Deg) + UnityEngine.Random.Range(0.0f, spread) - (spread / 2.0f))
					* Mathf.Deg2Rad;
				Rigidbody2D bullet = (UnityEngine.Object.Instantiate(projectile).gameObject).GetComponent<Rigidbody2D>();

				if(enemy.barrel != null) bullet.gameObject.transform.position = enemy.barrel.transform.position;
				else bullet.gameObject.transform.position = enemy.transform.position;

				bullet.velocity = (new Vector2(Mathf.Cos(angle), Mathf.Sin(angle))) * UnityEngine.Random.Range(7.5f, 12.5f);
			}

			return null;
		}
	}
}
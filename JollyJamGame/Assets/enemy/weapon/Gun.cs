using System;
using UnityEngine;

public class Gun : IWeapon
{
	protected Enemy enemy;
	protected Transform target;
	protected Projectile projectile;
	protected float fireRate;
	protected float velocity;

	protected float fireTimer;

	protected float direction;

	public Gun(Enemy enemy, Transform target, Projectile projectile, float fireRate, float velocity)
	{
		this.enemy = enemy;
		this.target = target;
		this.projectile = projectile;
		this.fireRate = fireRate;
		this.velocity = velocity;

		fireTimer = 0.0f;
	}
		
	public void aim(float timestep)
	{
		direction = Vector2.Angle (target.position, enemy.transform.position);
		fireTimer += timestep;
	}

	public virtual Projectile[] fire()
	{
		if (fireTimer <= fireRate) {
			return null;
		} else {
			Vector2 to = target.position - enemy.transform.position;
			float angle = (Mathf.Atan2(to.y, to.x) * Mathf.Rad2Deg + UnityEngine.Random.Range(-5.0f, 5.0f)) * Mathf.Deg2Rad;

			Rigidbody2D bullet = (UnityEngine.Object.Instantiate(projectile).gameObject).GetComponent<Rigidbody2D>();
			bullet.gameObject.transform.position = enemy.transform.position;
			bullet.velocity = (new Vector2(Mathf.Cos(angle), Mathf.Sin(angle))) * velocity;

			fireTimer = 0.0f;
			return null;
		}
	}
}
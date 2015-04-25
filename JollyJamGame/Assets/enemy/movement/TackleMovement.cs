using System;
using UnityEngine;

public class TackleMovement : IEnemyMovement
{
	private Enemy enemy;
	private Transform target;
	private float acceleration;

	private Rigidbody2D rigidbody2D;

	public TackleMovement(Enemy enemy, Transform target, float acceleration)
	{
		this.enemy = enemy;
		this.target = target;
		this.acceleration = acceleration;

		rigidbody2D = enemy.GetComponent<Rigidbody2D> ();
	}

	public void move(float timestep)
	{
		rigidbody2D.AddForce ((target.position - enemy.transform.position).normalized * acceleration, ForceMode2D.Force);
	}
}
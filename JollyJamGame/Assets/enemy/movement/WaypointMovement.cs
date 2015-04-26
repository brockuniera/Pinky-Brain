using System;
using UnityEngine;

public abstract class WaypointMovement : IEnemyMovement
{
	private static float MinimumWaypointDistanceThreshold = 4.0f;
	
	protected Enemy enemy;
	protected Transform target;
	protected float acceleration;
	
	protected Rigidbody2D rigidbody2D;

	protected Vector2 waypoint;
	
	private float WaypointDistanceThreshold;
	
	public WaypointMovement(Enemy enemy, Transform target, float acceleration)
	{
		this.enemy = enemy;
		this.target = target;
		this.acceleration = acceleration;
		
		rigidbody2D = enemy.GetComponent<Rigidbody2D> ();
		
		onWaypointReached ();
	}

	public abstract void onWaypointReached ();
	
	public virtual void move(float timestep)
	{
		WaypointDistanceThreshold = Mathf.Max (acceleration / 2.0f, MinimumWaypointDistanceThreshold);
		rigidbody2D.AddForce ((waypoint - (Vector2)enemy.transform.position).normalized * acceleration * Time.fixedDeltaTime,
		                      ForceMode2D.Force);
		
		if(Vector2.Distance(waypoint, enemy.transform.position) < WaypointDistanceThreshold)
		{
			onWaypointReached();
		}
	}
}
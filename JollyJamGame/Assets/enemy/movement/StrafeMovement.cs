using System;
using UnityEngine;

public class StrafeMovement : IEnemyMovement
{
	private static float MinimumWaypointDistanceThreshold = 4.0f;

	private Enemy enemy;
	private Transform target;
	public float radius;
	private float acceleration;

	private Rigidbody2D rigidbody2D;

	private float angle;
	private Vector2 waypoint;

	private float dangle;

	private float WaypointDistanceThreshold;

	public StrafeMovement(Enemy enemy, Transform target, float radius, float acceleration)
	{
		this.enemy = enemy;
		this.target = target;
		this.radius = radius;
		this.acceleration = acceleration;

		rigidbody2D = enemy.GetComponent<Rigidbody2D> ();

		angle = (Vector2.Angle (enemy.transform.position, target.position) + 90.0f) * Mathf.PI / 180.0f;
		waypoint = default(Vector2);

		dangle = UnityEngine.Random.Range (10.0f, 90.0f);
	}

	public void move(float timestep)
	{
		WaypointDistanceThreshold = Mathf.Max (radius / 4.0f, MinimumWaypointDistanceThreshold);

		rigidbody2D.AddForce ((waypoint - (Vector2)enemy.transform.position).normalized * acceleration, ForceMode2D.Force);

		if(Vector2.Distance(waypoint, enemy.transform.position) < WaypointDistanceThreshold)
		{
			radius += UnityEngine.Random.Range(-5.0f, 5.0f);
			angle += (dangle * Mathf.PI / 180.0f); angle %= Mathf.PI * 2;
			waypoint = (new Vector2 (Mathf.Cos (angle), Mathf.Sin (angle)) * radius) + (Vector2)target.position;
		}
	}
}
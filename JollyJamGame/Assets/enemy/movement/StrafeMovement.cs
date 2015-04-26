using System;
using UnityEngine;

public class StrafeMovement : WaypointMovement
{
	private int cw;
	public float radius;

	private float angle;
	private float dangle;

	public StrafeMovement(Enemy enemy, Transform target, float radius, float acceleration) : 
		base(enemy, target, acceleration)
	{
		cw = (UnityEngine.Random.Range (0, 2) == 0) ? -1 : 1;
		this.radius = radius;

		angle = (Vector2.Angle (enemy.transform.position, target.position) + 90.0f) * Mathf.PI / 180.0f;
		dangle = UnityEngine.Random.Range (10.0f, 180.0f);
	}

	public override void onWaypointReached()
	{
		radius += UnityEngine.Random.Range (-0.5f, 0.5f);
		angle += (dangle * Mathf.PI / 180.0f) * cw; angle %= Mathf.PI * 2;
		waypoint = (new Vector2 (Mathf.Cos (angle), Mathf.Sin (angle)) * radius) + (Vector2)target.position;
	}
}
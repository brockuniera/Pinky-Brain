using System;
using UnityEngine;

public class TackleMovement : WaypointMovement
{
	public TackleMovement(Enemy enemy, Transform target, float acceleration)
		: base(enemy, target, acceleration)
	{

	}

	public override void onWaypointReached()
	{
		waypoint = Vector2.Lerp(enemy.transform.position, target.transform.position, .5f);
	}
}

using System;
using UnityEngine;

public class EllipticalMovement : WaypointMovement
{
	private float width;
	private float height;

	private int cycle;
	private int cw;

	public EllipticalMovement(Enemy enemy, Transform target, float acceleration, float width, float height)
		: base(enemy, target, acceleration)
	{
		this.width = width;
		this.height = height;

		cycle = 0;
		cw = (UnityEngine.Random.Range (0, 2) == 0) ? -1 : 1;
	}

	public override void onWaypointReached ()
	{
		if (UnityEngine.Random.value < 0.3f)
			cw *= -1;

		switch (cycle) {
		case 0:
			waypoint = new Vector2(target.position.x + width, target.position.y);
			break;

		case 1:
			waypoint = new Vector2(target.position.x, target.position.y + height);
			break;

		case 2:
			waypoint = new Vector2(target.position.x - width, target.position.y);
			break;

		case 3:
			waypoint = new Vector2(target.position.x, target.position.y - height);
			break;
		}

		cycle += cw;
		cycle = Mathf.Abs (cycle % 4);
	}
}
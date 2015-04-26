using System;
using UnityEngine;

public interface IWeapon
{
	void aim(float timestep);
	Projectile[] fire();
}
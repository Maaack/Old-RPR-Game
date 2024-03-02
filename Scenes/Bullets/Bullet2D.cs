using Godot;
using System;

public partial class Bullet2D : RigidBody2D
{

	private void OnHitArea2DDamageInflicted(double damageAmount)
	{
		QueueFree();
	}

	private void OnTimerTimeout()
	{
		QueueFree();
	}


}



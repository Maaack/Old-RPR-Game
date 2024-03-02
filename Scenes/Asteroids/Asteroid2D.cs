using Godot;
using System;

public partial class Asteroid2D : RigidBody2D
{
	private void OnHurtArea2DDamageReceived(double damageAmount)
	{
		QueueFree();
	}
}



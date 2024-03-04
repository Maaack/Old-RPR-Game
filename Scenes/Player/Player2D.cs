using Godot;
using System;

public partial class Player2D : RigidBody2D
{
	private void OnHurtArea2DDamageReceived(double damageAmount, double damageAngle)
	{
		QueueFree();
	}
}

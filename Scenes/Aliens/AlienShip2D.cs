using Godot;
using System;

public partial class AlienShip2D : CharacterBody2D
{

	private bool Destroyed = false;

	public bool IsDestroyed()
	{
		return Destroyed;
	}

	private void Destroy()
	{
		if ( IsDestroyed() ) { return; }
		Destroyed = true;
		QueueFree();
	}
	private void OnHurtArea2DDamageReceived(double damageAmount, int damagingTeam, double damageAngle)
	{
		Destroy();
	}
	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		var children = GetChildren();
		foreach (Node child in children)
		{
			if ( child is AlienEngineComponent engineChild )
			{
				engineChild.PhysicsProcess(this);
			}
		}
		MoveAndSlide();
	}
}

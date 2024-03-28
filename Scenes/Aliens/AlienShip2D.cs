using Godot;
using System;

public partial class AlienShip2D : CharacterBody2D
{

	private bool Destroyed = false;
	private bool PlayerDestroyed = false;

	public bool IsDestroyed()
	{
		return Destroyed;
	}

	public bool IsPlayerDestroyed()
	{
		return PlayerDestroyed;
	}


	private void Destroy(int damagingTeam)
	{
		if ( IsDestroyed() ) { return; }
		if ( damagingTeam == (int)Constants.Teams.Player ){
			PlayerDestroyed = true;
		}
		Destroyed = true;
		QueueFree();
	}

	private void OnHurtArea2DDamageReceived(double damageAmount, int damagingTeam, double damageAngle)
	{
		Destroy(damagingTeam);
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

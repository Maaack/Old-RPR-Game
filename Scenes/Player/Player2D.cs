using Godot;
using System;

public partial class Player2D : RigidBody2D
{
	private bool active = false;
	private void OnHurtArea2DDamageReceived(double damageAmount, double damageAngle)
	{
		QueueFree();
	}

	public void SetActive()
	{
		if ( !active ) 
		{
			active = true;
			GetNode<AnimationPlayer>("SpawnAnimationPlayer").Play("RESET");
			GetNode<HurtArea2D>("HurtArea2D").Monitoring = true;
			GetNode<PilotInputComponent>("PilotInputComponent").Enabled = true;
		}
	}

	public override void _UnhandledInput(InputEvent @event)
	{
		base._UnhandledInput(@event);
		SetActive();
	}

	public override void _IntegrateForces(PhysicsDirectBodyState2D state)
	{
		base._IntegrateForces(state);
		var children = GetChildren();
		foreach (Node child in children)
		{
			if ( child is EngineComponent2D engineChild )
			{
				engineChild.IntegrateForces(state);
			}
		}
	}
}

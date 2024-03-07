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

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		var children = GetChildren();
		foreach (Node child in children)
		{
			if ( child is EngineComponent2D engineChild )
			{
				engineChild.PhysicsProcess(this);
			}
			else if ( child is StabilityAssistComponent stabilityAssistChild )
			{
				stabilityAssistChild.PhysicsProcess(this);
			}
		}
	}
}

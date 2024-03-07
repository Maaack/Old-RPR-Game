using Godot;
using System;
using System.ComponentModel.DataAnnotations;

public partial class StabilityAssistComponent : ComponentBase
{
	[Signal]
	public delegate void DirectionTriggeredEventHandler(int Direction);
	[Signal]
	public delegate void DirectionResetEventHandler(int Direction);
	[Export]
	public float TriggerAngularVelocity = (float)Math.PI * 2.0f;
	[Export]
	public float ResetAngularVelocity = (float)Math.PI * 1.5f;
	private bool RightSpinOn = false;
	private bool LeftSpinOn = false;

	public void PhysicsProcess(RigidBody2D body2D)
	{
		var AngularVelocity = body2D.AngularVelocity;
		if ( Math.Abs( AngularVelocity ) > TriggerAngularVelocity )
		{
			if ( AngularVelocity < 0 && !RightSpinOn )
			{
				RightSpinOn = true;
				EmitSignal(SignalName.DirectionTriggered, (int)Constants.Directions.Right);
			}
			else if ( AngularVelocity > 0 && !LeftSpinOn )
			{
				LeftSpinOn = true;
				EmitSignal(SignalName.DirectionTriggered, (int)Constants.Directions.Left);
			}
		} 
		else if ( Math.Abs( AngularVelocity ) < ResetAngularVelocity )
		{
			if ( RightSpinOn )
			{
				RightSpinOn = false;
				EmitSignal(SignalName.DirectionReset, (int)Constants.Directions.Right);
			}
			else if ( LeftSpinOn )
			{
				LeftSpinOn = false;
				EmitSignal(SignalName.DirectionReset, (int)Constants.Directions.Left);
			}
		}
	}
}

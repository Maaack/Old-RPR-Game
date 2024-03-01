using Godot;
using System;
using System.Collections.Generic;

public partial class PilotInputComponent : ComponentBase
{
	const String ForwardInputAction = "forward";
	const String BackwardInputAction = "backward";
	const String TurnLeftInputAction = "turn_left";
	const String TurnRightInputAction = "turn_right";

	public enum Directions
	{
		Forward,
		Back,
		Left,
		Right
	}

	private Godot.Collections.Dictionary ActionInputDirectionMap = new Godot.Collections.Dictionary{
		{ForwardInputAction, (int)Directions.Forward},
		{BackwardInputAction, (int)Directions.Back},
		{TurnLeftInputAction, (int)Directions.Left},
		{TurnRightInputAction, (int)Directions.Right},
	};
	[Signal]
	public delegate void DirectionPressedEventHandler(int Direction);
	[Signal]
	public delegate void DirectionReleasedEventHandler(int Direction); 
	public override void _Input(InputEvent @event)
	{
		foreach (var (inputAction, inputDirection) in ActionInputDirectionMap)
		{
			if (@event.IsActionPressed((string)inputAction))
			{
				EmitSignal(SignalName.DirectionPressed, inputDirection);
			} 
			else if (@event.IsActionReleased((string)inputAction))
			{
				EmitSignal(SignalName.DirectionReleased, inputDirection);
			}
		}
	}

	public override void _PhysicsProcess(double delta)
	{

	}
}

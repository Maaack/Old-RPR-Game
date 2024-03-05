using Godot;
using Godot.Collections;
using System;

public partial class EngineControlComponent : ComponentBase
{
	private PilotInputComponent pilotInputComponent;
	[Export]
	public PilotInputComponent PilotInputComponent
	{
		get {
			return pilotInputComponent;
		}
		set {
			pilotInputComponent = value;
			pilotInputComponent.DirectionPressed += OnDirectionPressed;
			pilotInputComponent.DirectionReleased += OnDirectionReleased;
		}
	}
	[Export]
	public Array<EngineComponent2D> ForwardEngines;
	[Export]
	public Array<EngineComponent2D> ReverseEngines;
	[Export]
	public Array<EngineComponent2D> TurnLeftEngines;
	[Export]
	public Array<EngineComponent2D> TurnRightEngines;
	private RigidBody2D body2D;
	[Export]
	public RigidBody2D Body2D
	{ 
		get {
			return body2D;
		}
		set {
			body2D = value;
		}
	}
	[Export]
	public float EngineForce = 1.0f;
	[Export]
	public float LinearForceMod = 1.0f;
	[Export]
	public float RotationalForceMod = 1.0f;
	private bool ForwardEngineOn = false;
	private bool ReverseEngineOn = false;
	private bool LeftTurnEngineOn = false;
	private bool RightTurnEngineOn = false;

	public override void _Ready(){
		if (body2D == null)
		{
			Body2D = GetNode<RigidBody2D>(".."); 
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		base._PhysicsProcess(delta);
		var finalVelocityVector = new Vector2();
		var finalRotationValue = 0.0f;
		if (ForwardEngineOn)
		{	
			finalVelocityVector += Vector2.Up.Rotated(body2D.Rotation);
		}
		if (ReverseEngineOn)
		{
			finalVelocityVector += Vector2.Down.Rotated(body2D.Rotation);
		}
		if (LeftTurnEngineOn)
		{
			finalRotationValue -= 1.0f;
		}
		if (RightTurnEngineOn)
		{
			finalRotationValue += 1.0f;
		}
		finalVelocityVector *= EngineForce * LinearForceMod;
		finalRotationValue *= EngineForce * RotationalForceMod;
		body2D.ApplyCentralImpulse(finalVelocityVector);
		body2D.ApplyTorqueImpulse(finalRotationValue);
	}

	private void OnDirectionPressed(int inputDirection)
	{
		switch((PilotInputComponent.Directions)inputDirection){
			case PilotInputComponent.Directions.Forward:
				foreach ( EngineComponent2D engine in ForwardEngines )
				{
					engine.Active = true;
				}
				break;
			case PilotInputComponent.Directions.Back:
				foreach ( EngineComponent2D engine in ReverseEngines )
				{
					engine.Active = true;
				}
				break;
			case PilotInputComponent.Directions.Left:
				LeftTurnEngineOn = true;
				break;
			case PilotInputComponent.Directions.Right:
				RightTurnEngineOn = true;
				break;
		}
	}

	private void OnDirectionReleased(int inputDirection)
	{
		switch((PilotInputComponent.Directions)inputDirection){
			case PilotInputComponent.Directions.Forward:
				foreach ( EngineComponent2D engine in ForwardEngines )
				{
					engine.Active = false;
				}
				break;
			case PilotInputComponent.Directions.Back:
				foreach ( EngineComponent2D engine in ReverseEngines )
				{
					engine.Active = false;
				}
				break;
			case PilotInputComponent.Directions.Left:
				LeftTurnEngineOn = false;
				break;
			case PilotInputComponent.Directions.Right:
				RightTurnEngineOn = false;
				break;

		}
	}
}

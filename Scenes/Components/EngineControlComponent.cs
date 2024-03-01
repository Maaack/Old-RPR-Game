using Godot;
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
	public float EngineForce = 100.0f;
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
		if (ForwardEngineOn)
		{
			finalVelocityVector += Vector2.Up;
		}
		if (ReverseEngineOn)
		{
			finalVelocityVector += Vector2.Down;
		}
		if (LeftTurnEngineOn)
		{
			finalVelocityVector += Vector2.Left;
		}
		if (RightTurnEngineOn)
		{
			finalVelocityVector += Vector2.Right;
		}
		finalVelocityVector *= EngineForce;
		body2D.ApplyCentralImpulse(finalVelocityVector);
	}

	private void OnDirectionPressed(int inputDirection)
	{
		switch((PilotInputComponent.Directions)inputDirection){
			case PilotInputComponent.Directions.Forward:
				ForwardEngineOn = true;
				break;
			case PilotInputComponent.Directions.Back:
				ReverseEngineOn = true;
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
				ForwardEngineOn = false;
				break;
			case PilotInputComponent.Directions.Back:
				ReverseEngineOn = false;
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

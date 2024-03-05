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
				foreach ( EngineComponent2D engine in TurnLeftEngines )
				{
					engine.Active = true;
				}
				break;
			case PilotInputComponent.Directions.Right:
				foreach ( EngineComponent2D engine in TurnRightEngines )
				{
					engine.Active = true;
				}
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
				foreach ( EngineComponent2D engine in TurnLeftEngines )
				{
					engine.Active = false;
				}
				break;
			case PilotInputComponent.Directions.Right:
				foreach ( EngineComponent2D engine in TurnRightEngines )
				{
					engine.Active = false;
				}
				break;

		}
	}
}

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
	private StabilityAssistComponent emergencyStabilityAssistComponent;
	[Export]
	public StabilityAssistComponent EmergencyStabilityAssistComponent
	{
		get {
			return emergencyStabilityAssistComponent;
		}
		set {
			emergencyStabilityAssistComponent = value;
			emergencyStabilityAssistComponent.DirectionTriggered += OnEmergencyTriggered;
			emergencyStabilityAssistComponent.DirectionReset += OnEmergencyReset;
		}
	}
	private StabilityAssistComponent idleStabilityAssistComponent;
	[Export]
	public StabilityAssistComponent IdleStabilityAssistComponent
	{
		get {
			return idleStabilityAssistComponent;
		}
		set {
			idleStabilityAssistComponent = value;
			idleStabilityAssistComponent.DirectionTriggered += OnIdleTriggered;
			idleStabilityAssistComponent.DirectionReset += OnIdleReset;
		}
	}
	private StabilityAssistComponent inputIgnoreStabilityAssistComponent;
	[Export]
	public StabilityAssistComponent InputIgnoreStabilityAssistComponent
	{
		get {
			return inputIgnoreStabilityAssistComponent;
		}
		set {
			inputIgnoreStabilityAssistComponent = value;
			inputIgnoreStabilityAssistComponent.DirectionTriggered += OnInputIgnoreTriggered;
			inputIgnoreStabilityAssistComponent.DirectionReset += OnInputIgnoreReset;
		}
	}	[Export]
	public Array<EngineComponent2D> ForwardEngines = new Array<EngineComponent2D>();
	[Export]
	public Array<EngineComponent2D> ReverseEngines = new Array<EngineComponent2D>();
	[Export]
	public Array<EngineComponent2D> TurnLeftEngines = new Array<EngineComponent2D>();
	[Export]
	public Array<EngineComponent2D> TurnRightEngines = new Array<EngineComponent2D>();
	private bool ForwardInputOn = false;
	private bool ReverseInputOn = false;
	private bool LeftInputOn = false;
	private bool RightInputOn = false;
	private bool LeftEmergencyOn = false;
	private bool RightEmergencyOn = false;
	private bool LeftIdleOn = false;
	private bool RightIdleOn = false;
	private bool LeftIgnoreInputOn = false;
	private bool RightIgnoreInputOn = false;
	private bool ForwardEngineOn = false;
	private bool ReverseEngineOn = false;
	private bool LeftTurnEngineOn = false;
	private bool RightTurnEngineOn = false;

	private bool UpdateTurnState(bool currentState, bool InputOn, bool EmergencyOn, bool IdleOn, bool InputIgnoreOn, bool OtherInputOn, bool OtherEmergencyOn, Array<EngineComponent2D> Engines)
	{
		if ( EmergencyOn ||
		( InputOn && !OtherEmergencyOn && !InputIgnoreOn) || 
		( IdleOn && !OtherInputOn && !OtherEmergencyOn ) )
		{
			if ( !currentState )
			{
				currentState = true;
				foreach ( EngineComponent2D engine in Engines )
				{
					engine.Active = true;
				}

			}
		} else if ( currentState ) 
		{
			currentState = false;
			foreach ( EngineComponent2D engine in Engines )
			{
				engine.Active = false;
			}

		}
		return currentState;
	}
	private void OnStateUpdated()
	{
		LeftTurnEngineOn = UpdateTurnState(LeftTurnEngineOn, LeftInputOn, LeftEmergencyOn, LeftIdleOn, LeftIgnoreInputOn, RightInputOn, RightEmergencyOn, TurnLeftEngines);
		RightTurnEngineOn = UpdateTurnState(RightTurnEngineOn, RightInputOn, RightEmergencyOn, RightIdleOn, RightIgnoreInputOn, LeftInputOn, LeftEmergencyOn, TurnRightEngines);
	}
	private void OnEmergencyTriggered(int inputDirection)
	{
		switch((Constants.Directions)inputDirection){
			case Constants.Directions.Left:
				LeftEmergencyOn = true;
				break;
			case Constants.Directions.Right:
				RightEmergencyOn = true;
				break;
		}
		OnStateUpdated();
	}

	private void OnEmergencyReset(int inputDirection)
	{
		switch((Constants.Directions)inputDirection){
			case Constants.Directions.Left:
				LeftEmergencyOn = false;
				break;
			case Constants.Directions.Right:
				RightEmergencyOn = false;
				break;
		}
		OnStateUpdated();
	}

	private void OnIdleTriggered(int inputDirection)
	{
		switch((Constants.Directions)inputDirection){
			case Constants.Directions.Left:
				LeftIdleOn = true;
				break;
			case Constants.Directions.Right:
				RightIdleOn = true;
				break;
		}
		OnStateUpdated();	
	}

	private void OnIdleReset(int inputDirection)
	{
		switch((Constants.Directions)inputDirection){
			case Constants.Directions.Left:
				LeftIdleOn = false;
				break;
			case Constants.Directions.Right:
				RightIdleOn = false;
				break;
		}
		OnStateUpdated();
	}

	private void OnInputIgnoreTriggered(int inputDirection)
	{
		switch((Constants.Directions)inputDirection){
			case Constants.Directions.Left:
				RightIgnoreInputOn = true;
				break;
			case Constants.Directions.Right:
				LeftIgnoreInputOn = true;
				break;
		}
		OnStateUpdated();	
	}

	private void OnInputIgnoreReset(int inputDirection)
	{
		switch((Constants.Directions)inputDirection){
			case Constants.Directions.Left:
				RightIgnoreInputOn = false;
				break;
			case Constants.Directions.Right:
				LeftIgnoreInputOn = false;
				break;
		}
		OnStateUpdated();
	}

	private void OnDirectionPressed(int inputDirection)
	{
		switch((Constants.Directions)inputDirection){
			case Constants.Directions.Forward:
				ForwardInputOn = true;
				foreach ( EngineComponent2D engine in ForwardEngines )
				{
					engine.Active = true;
				}
				break;
			case Constants.Directions.Back:
				ReverseInputOn = true;
				foreach ( EngineComponent2D engine in ReverseEngines )
				{
					engine.Active = true;
				}
				break;
			case Constants.Directions.Left:
				LeftInputOn = true;
				break;
			case Constants.Directions.Right:
				RightInputOn = true;
				break;
		}
		OnStateUpdated();
	}

	private void OnDirectionReleased(int inputDirection)
	{
		switch((Constants.Directions)inputDirection){
			case Constants.Directions.Forward:
				ForwardInputOn = false;
				foreach ( EngineComponent2D engine in ForwardEngines )
				{
					engine.Active = false;
				}
				break;
			case Constants.Directions.Back:
				ReverseInputOn = false;
				foreach ( EngineComponent2D engine in ReverseEngines )
				{
					engine.Active = false;
				}
				break;
			case Constants.Directions.Left:
				LeftInputOn = false;
				break;
			case Constants.Directions.Right:
				RightInputOn = false;
				break;
		}
		OnStateUpdated();
	}
}

using Godot;
using System;

public partial class AlienEngineComponent : ComponentBase
{
	[Export]
	public Vector2 CentralDirection = Vector2.Right;
	[Export]
	public float EngineForce = 1.0f;
	private AlienInputComponent alienInputComponent;
	[Export]
	public AlienInputComponent AlienInputComponent
		{
		get {
			return alienInputComponent;
		}
		set {
			alienInputComponent = value;
			alienInputComponent.EngineDirectionUpdated += (double direction) => EngineDirection = direction ;
		}
	}
	public bool Active = true;
	private double EngineDirection;

	public void PhysicsProcess(CharacterBody2D body2D)
	{
		if ( !Active ) { return; }
		var forceVector = EngineForce * CentralDirection.Rotated((float)EngineDirection);
		body2D.Velocity = forceVector;
	}
}

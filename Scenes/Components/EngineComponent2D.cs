using Godot;
using System;

public partial class EngineComponent2D : ComponentBase2D
{
	[Export]
	public float EngineForce = 1.0f;
	public bool Active = false;
	public void PhysicsProcess(RigidBody2D body2D)
	{
		var SpriteNode = GetNode<Sprite2D>("Sprite2D");
		SpriteNode.Hide();
		if ( !Active ) { return; }
		var forceVector = EngineForce * Vector2.Up.Rotated(GlobalRotation);
		body2D.ApplyImpulse(forceVector, Position.Rotated(GlobalRotation - Rotation));
		SpriteNode.Show();
	}
}

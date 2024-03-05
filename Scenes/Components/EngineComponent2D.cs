using Godot;
using System;

public partial class EngineComponent2D : ComponentBase2D
{
	[Export]
	public float EngineForce = 1.0f;
	public bool Active = false;
	public void IntegrateForces(PhysicsDirectBodyState2D state2D)
	{
		var SpriteNode = GetNode<Sprite2D>("Sprite2D");
		SpriteNode.Hide();
		if ( !Active ) { return; }
		var forceVector = EngineForce * Vector2.Up.Rotated(GlobalRotation);
		state2D.ApplyImpulse(forceVector, Position.Rotated(GlobalRotation - Rotation));
		SpriteNode.Show();
	}
}

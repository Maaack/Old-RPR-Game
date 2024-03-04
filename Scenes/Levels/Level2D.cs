using Godot;
using System;

public partial class Level2D : Node2D
{
	[Signal]
	public delegate void LevelWonEventHandler();
	[Signal]
	public delegate void LevelLostEventHandler();
	[Signal]
	public delegate void ScoreUpdatedEventHandler(int deltaValue);
	[Export]
	public Vector2 WorldSize = new Vector2(640, 360);
	[Export]
	public float WorldSizeMod = 1.0f;
	[Export]
	public PackedScene AsteroidScene;
	protected Node2D Player;
	
	private int totalAsteroidCount;
	private int destroyedAsteroidCount;

	private bool AllAsteroidsDestroyed()
	{
		return totalAsteroidCount <= destroyedAsteroidCount;
	}

	private void CheckLevelSuccess()
	{
		if (AllAsteroidsDestroyed())
		{
			EmitSignal(SignalName.LevelWon);
		}
	}

	private void DestroyAsteroid(Node node)
	{
		if ( node is Asteroid2D asteroidNode) {
			if ( !asteroidNode.IsDestroyed() ) { return; }
			destroyedAsteroidCount += 1;
			EmitSignal(SignalName.ScoreUpdated, 10);
			CheckLevelSuccess();
		}
	}

	private void ConnectAsteroid(Node body2D)
	{
		if ( body2D is Asteroid2D asteroidChild )
		{
			totalAsteroidCount += 1;
			if (AsteroidScene != null)
			{
				asteroidChild.PartScene = AsteroidScene;
			}
		}
	}

	private void OnChildEnteredTree(Node node)
	{
		ConnectAsteroid(node);
	}

	private void OnChildExitedTree(Node node)
	{
		DestroyAsteroid(node);
	}

	public override void _Ready()
	{
		Player = GetNode<Node2D>("%Player2D");
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		var childNodes = GetChildren();
		var fullWorldSize = WorldSizeMod * WorldSize;
		foreach ( Node2D child in childNodes )
		{
			if ( child is RigidBody2D rigidChild )
			{
				var translatedVector = rigidChild.Position;
				if ( rigidChild.Position.X < - fullWorldSize.X / 2 )
				{
					translatedVector.X = fullWorldSize.X / 2 + (rigidChild.Position.X + fullWorldSize.X / 2);
				} 
				else if ( rigidChild.Position.X > fullWorldSize.X / 2 )
				{
					translatedVector.X = -fullWorldSize.X / 2 + (rigidChild.Position.X - fullWorldSize.X / 2);
				}
				if ( rigidChild.Position.Y < - fullWorldSize.Y / 2  )
				{
					translatedVector.Y = fullWorldSize.Y / 2 + (rigidChild.Position.Y + fullWorldSize.Y / 2);
				} 
				else if ( rigidChild.Position.Y > fullWorldSize.Y / 2 )
				{
					translatedVector.Y = -fullWorldSize.Y / 2 + (rigidChild.Position.Y - fullWorldSize.Y / 2);
				}
				if (!(translatedVector - rigidChild.Position).IsZeroApprox())
				{
					PhysicsServer2D.BodySetState(
						rigidChild.GetRid(),
						PhysicsServer2D.BodyState.Transform,
						new Transform2D(rigidChild.Rotation, translatedVector)
					);
				}
			}
		}
	}
}

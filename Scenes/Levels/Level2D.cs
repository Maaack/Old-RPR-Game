using Godot;
using System;

public partial class Level2D : Node2D
{
	[Signal]
	public delegate void LevelWonEventHandler();
	[Signal]
	public delegate void LevelLostEventHandler();
	[Signal]
	public delegate void ScoreUpdatedEventHandler(int scoreDelta);
	[Signal]
	public delegate void LivesUpdatedEventHandler(int livesValue);
	[Export]
	public Vector2 WorldSize = new Vector2(640, 360);
	[Export]
	public float WorldSizeMod = 1.0f;
	[Export]
	public int PlayerLives = 3;
	[Export]
	public PackedScene PlayerScene;
	[Export]
	public PackedScene AsteroidScene;
	protected Player2D Player;
	private int currentPlayerLives;
	public int CurrentPlayerLives 
	{
		get {
			return currentPlayerLives;
		}
		set {
			currentPlayerLives = value;
			EmitSignal(SignalName.LivesUpdated, currentPlayerLives);
		}
	}
	private int totalAsteroidCount;
	private int destroyedAsteroidCount;

	private bool AllAsteroidsDestroyed()
	{
		return totalAsteroidCount <= destroyedAsteroidCount;
	}

	private bool HasPlayerLives()
	{
		return currentPlayerLives > 0;
	}

	private void CheckLevelSuccess()
	{
		if (AllAsteroidsDestroyed())
		{
			EmitSignal(SignalName.LevelWon);
		}
	}

	private void DestroyPlayer(Node node)
	{
		if ( node is Player2D playerNode )
		{
			if ( HasPlayerLives() && PlayerScene != null)
			{
				CurrentPlayerLives -= 1;
				var playerInstance = PlayerScene.Instantiate();
				CallDeferred("add_child", playerInstance);
				return;
			}
			EmitSignal(SignalName.LevelLost);
		}
	}

	private void DestroyAsteroid(Node node)
	{
		if ( node is Asteroid2D asteroidNode) {
			if ( !asteroidNode.IsDestroyed() ) { return; }
			destroyedAsteroidCount += 1;
			if ( asteroidNode.IsPlayerDestroyed() ) {
				EmitSignal(SignalName.ScoreUpdated, 10);
			}
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
		DestroyPlayer(node);
	}

	public override void _Ready()
	{
		Player = GetNode<Player2D>("%Player2D");
		Player.SetActive();
		CurrentPlayerLives = PlayerLives;
	}

	public override void _Process(double delta)
	{
		base._Process(delta);
		var childNodes = GetChildren();
		var fullWorldSize = WorldSizeMod * WorldSize;
		foreach ( Node2D child in childNodes )
		{
			if ( child is PhysicsBody2D physicsChild )
			{
				var translatedVector = physicsChild.Position;
				if ( physicsChild.Position.X < - fullWorldSize.X / 2 )
				{
					translatedVector.X = fullWorldSize.X / 2 + (physicsChild.Position.X + fullWorldSize.X / 2);
				} 
				else if ( physicsChild.Position.X > fullWorldSize.X / 2 )
				{
					translatedVector.X = -fullWorldSize.X / 2 + (physicsChild.Position.X - fullWorldSize.X / 2);
				}
				if ( physicsChild.Position.Y < - fullWorldSize.Y / 2  )
				{
					translatedVector.Y = fullWorldSize.Y / 2 + (physicsChild.Position.Y + fullWorldSize.Y / 2);
				} 
				else if ( physicsChild.Position.Y > fullWorldSize.Y / 2 )
				{
					translatedVector.Y = -fullWorldSize.Y / 2 + (physicsChild.Position.Y - fullWorldSize.Y / 2);
				}
				if (!(translatedVector - physicsChild.Position).IsZeroApprox())
				{
					if ( physicsChild is RigidBody2D rigidChild )
					{
						PhysicsServer2D.BodySetState(
							rigidChild.GetRid(),
							PhysicsServer2D.BodyState.Transform,
							new Transform2D(rigidChild.Rotation, translatedVector)
						);
					}
					else if ( physicsChild is CharacterBody2D characterChild )
					{
						characterChild.Position = translatedVector;
					}
				}
			}
		}
	}
}

using Godot;
using System;

public partial class Asteroid2D : RigidBody2D
{
	public enum Sizes {
		Smallest,
		Small,
		Medium,
		Large,
		Largest,
	}

	[Export]
	public PackedScene PartScene;
	[Export]
	public Sizes Size = Sizes.Large;

	private Godot.Collections.Dictionary SizeScaleMap = new Godot.Collections.Dictionary{
		{(int)Sizes.Smallest, 0.125f},
		{(int)Sizes.Small, 0.25f},
		{(int)Sizes.Medium, 0.5f},
		{(int)Sizes.Large, 1.0f},
		{(int)Sizes.Largest, 2.0f},
	};
	
	private RigidBody2D SpawnPart()
	{
		var partInstance = PartScene.Instantiate<RigidBody2D>();
		partInstance.Transform = GetTransform();
		partInstance.LinearVelocity = LinearVelocity;
		partInstance.AngularVelocity = AngularVelocity * 2;
		if (partInstance is Asteroid2D asteroidPart)
		{
			asteroidPart.Size = Size - 1;
		}
		GetParent().CallDeferred("add_child", partInstance);
		return partInstance;
	}
	
	private void OnHurtArea2DDamageReceived(double damageAmount)
	{
		QueueFree();
		if ( Size > 0 )
		{
			var partInstance = SpawnPart();
		}
	}

	private void UpdateSpriteScale()
	{
		var spriteNode = GetNode<Sprite2D>("Sprite2D");
		var scale = (float)SizeScaleMap[(int)Size];
		spriteNode.Scale = Vector2.One * scale;
	}

	public override void _Ready()
	{
		base._Ready();
		UpdateSpriteScale();
	}
}



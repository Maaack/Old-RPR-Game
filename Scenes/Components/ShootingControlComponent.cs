using Godot;
using System;

public partial class ShootingControlComponent : Node2D
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
			pilotInputComponent.PrimaryFirePressed += OnPrimaryFirePressed;
			pilotInputComponent.PrimaryFireReleased += OnPrimaryFireReleased;
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
	public PackedScene BulletScene;
	[Export]
	public float GunForce = 1.0f;
	public float CoolDownTime = 1.0f;
	private bool PrimaryFireIsCooled = true;
	private bool PrimaryFireOn = false;

	private void OnPrimaryFirePressed()
	{
		PrimaryFireOn = true;
	}

	private void OnPrimaryFireReleased()
	{
		PrimaryFireOn = false;
	}

	private async void HeatUpPrimaryWeapon()
	{
		PrimaryFireIsCooled = false;
		await ToSignal(GetTree().CreateTimer(CoolDownTime), Timer.SignalName.Timeout);
		PrimaryFireIsCooled = true;
	}

	private void FirePrimaryWeapon()
	{
		var bulletInstance = BulletScene.Instantiate<RigidBody2D>();
		bulletInstance.Transform = body2D.GetTransform();
		bulletInstance.LinearVelocity = body2D.LinearVelocity + (Vector2.Up.Rotated(body2D.Rotation) * GunForce);
		body2D.GetParent().AddChild(bulletInstance);
		HeatUpPrimaryWeapon();
	}
	
	public override void _Ready(){
		if (body2D == null)
		{
			Body2D = GetNode<RigidBody2D>(".."); 
		}
	}
	public override void _Process(double delta)
	{
		base._Process(delta);
		if ( PrimaryFireOn && PrimaryFireIsCooled )
		{
			FirePrimaryWeapon();
		}
	}
}

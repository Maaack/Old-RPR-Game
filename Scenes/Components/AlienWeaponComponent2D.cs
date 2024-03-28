using Godot;
using System;

public partial class AlienWeaponComponent2D : ComponentBase2D
{
	private AlienInputComponent alienInputComponent;
	[Export]
	public AlienInputComponent AlienInputComponent
	{
		get {
			return alienInputComponent;
		}
		set {
			alienInputComponent = value;
			alienInputComponent.WeaponDirectionUpdated += (double direction) => weaponDirection = direction ;
		}
	}
	private double weaponDirection;
	private Timer fireWeaponTimer;
	[Export]
	public Timer FireWeaponTimer
	{
		get {
			return fireWeaponTimer;
		}
		set {
			fireWeaponTimer = value;
			fireWeaponTimer.Timeout += FirePrimaryWeapon;
		}
	}
	private CharacterBody2D body2D;
	[Export]
	public CharacterBody2D Body2D
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

	private void FirePrimaryWeapon()
	{
		var bulletInstance = BulletScene.Instantiate<RigidBody2D>();
		bulletInstance.GlobalTransform = GetGlobalTransform();
		bulletInstance.Rotation = (float)weaponDirection;
		bulletInstance.LinearVelocity = Vector2.Up.Rotated((float)weaponDirection) * GunForce;
		body2D.GetParent().AddChild(bulletInstance);
	}

	public override void _Ready(){
		if (body2D == null)
		{
			Body2D = GetNode<CharacterBody2D>(".."); 
		}
	}
}

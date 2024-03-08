using Godot;
using System;

public partial class AlienInputComponent : ComponentBase
{
	[Signal]
	public delegate void EngineDirectionUpdatedEventHandler(double Angle);
	[Signal]
	public delegate void WeaponDirectionUpdatedEventHandler(double Angle);
	[Export]
	public bool Enabled = true;
	private Timer engineDirectionTimer;
	[Export]
	public Timer EngineDirectionTimer
	{
		get {
			return engineDirectionTimer;
		}
		set {
			engineDirectionTimer = value;
			engineDirectionTimer.Timeout += UpdateEngineDirection;
		}
	}
	private Timer weaponDirectionTimer;
	[Export]
	public Timer WeaponDirectionTimer
	{
		get {
			return weaponDirectionTimer;
		}
		set {
			weaponDirectionTimer = value;
			weaponDirectionTimer.Timeout += UpdateWeaponDirection;
		}
	}
	private double currentEngineDirection = 0.0;
	private double currentWeaponDirection = 0.0;
	

	private void UpdateEngineDirection()
	{
		var newEngineDirection = currentEngineDirection;
		var random = new Random();
		if ( currentEngineDirection == 0.0 ) {
			switch( random.Next(0,3) )
			{
				case 1:
					newEngineDirection = 0.6;
					break;
				case 2:
					newEngineDirection = -0.6;
					break;
			}
		}
		else
		{
			switch( random.Next(0,2) )
			{
				case 1:
					newEngineDirection = 0.0;
					break;
			}

		}
		if ( newEngineDirection != currentEngineDirection )
		{
			currentEngineDirection = newEngineDirection;
			EmitSignal(SignalName.EngineDirectionUpdated, currentEngineDirection);
		}
	}

	private void UpdateWeaponDirection()
	{
		var random = new Random();
		var newWeaponDirection = random.NextDouble() * 2 * Math.PI;
		if ( newWeaponDirection != currentWeaponDirection )
		{
			currentWeaponDirection = newWeaponDirection;
			EmitSignal(SignalName.WeaponDirectionUpdated, currentWeaponDirection);
		}
	}
	public override void _Process(double delta)
	{
		base._Process(delta);
		if ( !Enabled ) { return; }

	}


}

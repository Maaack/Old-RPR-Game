using Godot;
using System;

public partial class HitArea2D : Area2D
{
	[Signal]
	public delegate void DamageInflictedEventHandler(float damageAmount);
	[Export]
	public float Damage = 1.0f;

	public void DamageDealt(float damageAmount)
	{
		EmitSignal(SignalName.DamageInflicted, damageAmount);
	}
}

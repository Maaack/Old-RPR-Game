using Godot;
using System;

public partial class HurtArea2D : Area2D
{
	[Signal]
	public delegate void DamageReceivedEventHandler(float damageAmount, float damageAngle);
	private void OnAreaEntered(Area2D area)
	{
		if ( area is HitArea2D hitArea )
		{
			var damageReceived = hitArea.Damage;
			var damageAngle = (hitArea.GlobalPosition - GlobalPosition).Angle();
			EmitSignal(SignalName.DamageReceived, damageReceived, damageAngle);
			hitArea.DamageDealt(damageReceived);
		}
	}

}



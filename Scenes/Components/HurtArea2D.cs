using Godot;
using System;

public partial class HurtArea2D : Area2D
{
	[Signal]
	public delegate void DamageReceivedEventHandler(float damageAmount, int damagingTeam, float damageAngle);
	private void OnAreaEntered(Area2D area)
	{
		if ( area is HitArea2D hitArea )
		{
			var damageReceived = hitArea.Damage;
			var damagingTeam = (int)hitArea.Team;
			var damageAngle = (hitArea.GlobalPosition - GlobalPosition).Angle();
			EmitSignal(SignalName.DamageReceived, damageReceived, damagingTeam, damageAngle);
			hitArea.DamageDealt(damageReceived);
		}
	}

}



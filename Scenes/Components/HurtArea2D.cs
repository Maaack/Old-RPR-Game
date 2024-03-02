using Godot;
using System;

public partial class HurtArea2D : Area2D
{
	[Signal]
	public delegate void DamageReceivedEventHandler(float damageAmount);
	private void OnAreaEntered(Area2D area)
	{
		if ( area is HitArea2D hitArea )
		{
			var damageReceived = hitArea.Damage;
			EmitSignal(SignalName.DamageReceived, damageReceived);
			hitArea.DamageDealt(damageReceived);
		}
	}

}



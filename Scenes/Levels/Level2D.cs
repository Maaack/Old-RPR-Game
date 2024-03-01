using Godot;
using System;

public partial class Level2D : Node2D
{
	[Signal]
	public delegate void LevelWonEventHandler();
	[Signal]
	public delegate void LevelLostEventHandler(); 
	protected Node2D Player;
	public override void _Ready()
	{
		Player = GetNode<Node2D>("%Player2D");
	}
}

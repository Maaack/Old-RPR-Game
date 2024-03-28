using Godot;
using System;

public static partial class Constants : Object
{
	public enum Directions
	{
		Forward,
		Back,
		Left,
		Right
	}

	public enum Teams
	{
		None,
		Asteroid,
		Enemy,
		Player
	}

}

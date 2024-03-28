using Godot;
using Godot.Collections;
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

	public const int AsteroidPointValue = 10;
	public const int AlienPointValue = 50;

}

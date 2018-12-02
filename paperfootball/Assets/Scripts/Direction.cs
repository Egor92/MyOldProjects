using UnityEngine;
using System;

public struct Direction
{
    Direction.Enum data;
        
    public bool IsDiagonal
    {
        get
        {
            switch (data)
            {
                case Direction.Enum.UpLeft:
                case Direction.Enum.UpRight:
                case Direction.Enum.DownRight:
                case Direction.Enum.DownLeft:
                    return true;
                default:
                    return false;
            }
        }
    }
	
	public void InverseX()
	{
		switch (data)
		{
			case Direction.Enum.Left:      data = Direction.Enum.Right; break;
			case Direction.Enum.UpLeft:    data = Direction.Enum.UpRight; break;
			case Direction.Enum.Up:        data = Direction.Enum.Up; break;
			case Direction.Enum.UpRight:   data = Direction.Enum.UpLeft; break;
			case Direction.Enum.Right:     data = Direction.Enum.Left; break;
			case Direction.Enum.DownRight: data = Direction.Enum.DownLeft; break;
			case Direction.Enum.Down:      data = Direction.Enum.Down; break;
			case Direction.Enum.DownLeft:  data = Direction.Enum.DownRight; break;
			default:
				throw new Exception();
		}
	}
	
	public void InverseY()
	{
		switch (data)
		{
			case Direction.Enum.Left:      data = Direction.Enum.Left; break;
			case Direction.Enum.UpLeft:    data = Direction.Enum.DownLeft; break;
			case Direction.Enum.Up:        data = Direction.Enum.Down; break;
			case Direction.Enum.UpRight:   data = Direction.Enum.DownRight; break;
			case Direction.Enum.Right:     data = Direction.Enum.Right; break;
			case Direction.Enum.DownRight: data = Direction.Enum.UpRight; break;
			case Direction.Enum.Down:      data = Direction.Enum.Up; break;
			case Direction.Enum.DownLeft:  data = Direction.Enum.UpLeft; break;
			default:
				throw new Exception();
		}
	}

    public Vector2 ToVector2()
    {
        switch (data)
        {
            case Direction.Enum.Left:      return -Vector2.right;
            case Direction.Enum.UpLeft:    return Vector2.up - Vector2.right;
            case Direction.Enum.Up:        return Vector2.up;
            case Direction.Enum.UpRight:   return Vector2.up + Vector2.right;
            case Direction.Enum.Right:     return Vector2.right;
            case Direction.Enum.DownRight: return -Vector2.up + Vector2.right;
            case Direction.Enum.Down:      return -Vector2.up;
            case Direction.Enum.DownLeft:  return -Vector2.up - Vector2.right;
            default:
                return Vector2.zero;
        }
    }

    private Direction(Direction.Enum data)
    {
        this.data = data;
    }

    public static implicit operator Vector2(Direction direction)
    {
        return direction.ToVector2();
    }
    
    public static implicit operator Direction(Direction.Enum direction)
    {
        return new Direction(direction);
    }
    
    public static Vector2 operator*(Direction direction, float value)
    {
        return direction.ToVector2() * value;
    }
    
    public static Direction operator-(Direction direction)
    {
        switch (direction.data)
        {
            case Direction.Enum.Left:      return Direction.Enum.Right;
            case Direction.Enum.UpLeft:    return Direction.Enum.DownRight;
            case Direction.Enum.Up:        return Direction.Enum.Down;
            case Direction.Enum.UpRight:   return Direction.Enum.DownLeft;
            case Direction.Enum.Right:     return Direction.Enum.Left;
            case Direction.Enum.DownRight: return Direction.Enum.UpLeft;
            case Direction.Enum.Down:      return Direction.Enum.Up;
            case Direction.Enum.DownLeft:  return Direction.Enum.UpRight;
            default:
                throw new Exception();
        }
    }
    

    public static readonly Direction Left =      Direction.Enum.Left;
    public static readonly Direction UpLeft =    Direction.Enum.UpLeft;
    public static readonly Direction Up =        Direction.Enum.Up;    
    public static readonly Direction UpRight =   Direction.Enum.UpRight;
    public static readonly Direction Right =     Direction.Enum.Right;
    public static readonly Direction DownRight = Direction.Enum.DownRight;
    public static readonly Direction Down =      Direction.Enum.Down;
    public static readonly Direction DownLeft =  Direction.Enum.DownLeft;

    public static readonly Direction[] All = { Left, UpLeft, Up, UpRight, Right, DownRight, Down, DownLeft };

    public enum Enum
    {
        Left,
        UpLeft,
        Up,
        UpRight,
        Right,
        DownRight,
        Down,
        DownLeft
    }
}
using UnityEngine;
using System;

public class DirectionButton : MonoBehaviour
{
    public Direction.Enum direction;
    
    private Controls controls;

    void Start()
    {
        controls = GetComponentInParent<Controls>();
    }

    void OnMouseDown()
    {
        controls.Command.Invoke(direction);
    }
}

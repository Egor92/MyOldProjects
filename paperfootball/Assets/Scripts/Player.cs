using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour
{
    public Color Color;

    [HideInInspector]
    public GameController Game;
	public LinesDrawer LineDrawer { get { return lineDrawer; }}

	private	LinesDrawer lineDrawer;
    private Controls controls;

    void Start()
    {
		lineDrawer = this.GetComponentInChildren<LinesDrawer>();
		lineDrawer.Color = this.Color;

        controls = this.GetComponentInChildren<Controls>();
        controls.gameObject.SetActive(false);
    }

    public void StartTurn()
    {
        controls.gameObject.SetActive(true);
        controls.Command = OnButtonPressed;
    }

    public void OnButtonPressed(Direction direction)
    {
        Game.StartCoroutine("ProcessTurn", direction);
    }


    public void EndTurn()
    {
        controls.gameObject.SetActive(false);
    }
}

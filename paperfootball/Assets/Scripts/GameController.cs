using UnityEngine;
using System.Collections.Generic;
using System;
using System.Linq;
using System.Collections;

[Flags]
public enum FieldState : int
{
	Empty = 0,
	Player1 = 1,
	Player2 = 2,
	Gates = 4,
	Border = 8
}

public class GameController : MonoBehaviour
{
    #region Parameters
	public LineRenderer LinePrefab;
	public SpriteRenderer StartPointPrefab;
	public SpriteRenderer SmallPointPrefab;
	public Color BorderColor;
	public Player Player1;
	public Player Player2;
	public int FieldWidth;
	public int FieldHeight;
	public int GatesWidth;
	public int TurnLength;
	public int LongShotLength;
    #endregion
    
	private FieldState[,] gameField;
	private FieldState[,] fieldsCenters;
	private Player currentPlayer;
	private Vector2 currentPosition;
	private int player1Score = 0;
	private int player2Score = 0;
	private IEnumerable<Direction> availableDirections;
	private bool isLongShot = false;
	private int currentTurnLengthCounter = 0;

	void Start()
	{
		Player1.Game = this;
		Player2.Game = this;

		NewGame();
	}

	void OnGUI()
	{
		GUI.Label(new Rect(10,10,100,20), string.Format("{0} : {1}", player1Score, player2Score));
	}

	void NewGame()
	{
		for (int i = 0; i < this.transform.childCount; i++)
		{
			Destroy(this.transform.GetChild(i).gameObject);
		}

		InitGameField();
		
		PlaceBorders();
		PlaceGates();
		PlaceStartPoint();

		isLongShot = false;
		currentPlayer = null;

		StartCoroutine("NextTurn", true);
	}

    #region Initialization
	void InitGameField()
	{
		gameField = new FieldState[FieldWidth + 1, FieldHeight + 1];
		fieldsCenters = new FieldState[FieldWidth, FieldHeight];
	}
    
	void PlaceBorders()
	{        
		for (int i = 0; i <= FieldWidth; i++)
		{
			for (int j = 0; j <= FieldHeight; j++)
			{
				if (i == 0 || i == FieldWidth || j == 0 || j == FieldHeight)
					gameField [i, j] = FieldState.Border;
				else
					gameField [i, j] = FieldState.Empty;
			}
		}

		DrawLine(Vector2.zero, new Vector2(0f, FieldHeight), BorderColor);
		DrawLine(Vector2.zero, new Vector2(FieldWidth, 0f), BorderColor);
		DrawLine(new Vector2(0f, FieldHeight), new Vector2(FieldWidth, FieldHeight), BorderColor);
		DrawLine(new Vector2(FieldWidth, 0f), new Vector2(FieldWidth, FieldHeight), BorderColor);
	}

	void PlaceGates()
	{
		int leftRod = FieldWidth / 2 - GatesWidth / 2;
		int rightRod = FieldWidth / 2 + GatesWidth / 2;

		var tlRod = new Vector2(leftRod, FieldHeight);
		DrawLine(tlRod, tlRod - new Vector2(0f, 1f), Player1.Color);
		SetFieldAtPos(tlRod - new Vector2(0f, 1f), FieldState.Border);

		var trRod = new Vector2(rightRod, FieldHeight);
		DrawLine(trRod, trRod - new Vector2(0f, 1f), Player1.Color);
		SetFieldAtPos(trRod - new Vector2(0f, 1f), FieldState.Border);

		for (int i = leftRod; i <= rightRod; i++)
		{
			gameField [i, FieldHeight] = FieldState.Gates | FieldState.Player1;
		}

		fieldsCenters [leftRod - 1, FieldHeight - 1] = FieldState.Border;
		fieldsCenters [rightRod, FieldHeight - 1] = FieldState.Border;


		var blRod = new Vector2(leftRod, 0);
		DrawLine(blRod, blRod + new Vector2(0f, 1f), Player2.Color);
		SetFieldAtPos(blRod + new Vector2(0f, 1f), FieldState.Border);
        
		var brRod = new Vector2(rightRod, 0);
		DrawLine(brRod, brRod + new Vector2(0f, 1f), Player2.Color);
		SetFieldAtPos(brRod + new Vector2(0f, 1f), FieldState.Border);
                
		for (int i = leftRod; i <= rightRod; i++)
		{
			gameField [i, 0] = FieldState.Gates | FieldState.Player2;
		}
        
		fieldsCenters [leftRod - 1, 0] = FieldState.Border;
		fieldsCenters [rightRod, 0] = FieldState.Border;
	}

	void PlaceStartPoint()
	{
		var center = new Vector2(FieldWidth / 2, FieldHeight / 2);
		SetFieldAtPos(center, FieldState.Player1);
		var startPoint = Instantiate(StartPointPrefab, center, new Quaternion()) as SpriteRenderer;
		startPoint.color = Player1.Color;
		startPoint.transform.parent = transform;

		currentPosition = center;
	}
    #endregion

	public void NextTurn(bool switchPlayer = true)
	{
		currentTurnLengthCounter = 0;

		if (availableDirections == null)
			availableDirections = GetAvailableDirections();

		if (currentPlayer != null)
		{
			DrawPoint((Vector3)currentPosition - new Vector3(0f,0f,1f), currentPlayer.Color);
			currentPlayer.EndTurn();
		}

		if (switchPlayer)
			SwitchPlayer();

		currentPlayer.StartTurn();
	}

	void SwitchPlayer()
	{
		if (currentPlayer == null || currentPlayer == Player2)
			currentPlayer = Player1;
		else
			currentPlayer = Player2;
	}

	public void ProcessTurn(Direction direction)
	{        
		if (isLongShot)
		{
            while (currentTurnLengthCounter <= LongShotLength)
            {                
                var newPosition = currentPosition + direction;
                
                var currValue = GetFieldAtPos(newPosition);
                
				SetFlagAtPos(newPosition, currValue | (currentPlayer == Player1 ? FieldState.Player1 : FieldState.Player2));
                
                if (direction.IsDiagonal)
                    SetFieldAtPos(currentPosition + direction * 0.5f, currentPlayer == Player1 ? FieldState.Player1 : FieldState.Player2);
                
                //DrawLine(currentPosition, newPosition, currentPlayer.Color);
				currentPlayer.LineDrawer.DrawLine(currentPosition, newPosition);


                currentPosition = newPosition;
                
                if (currValue.HasFlag(FieldState.Border))
				{
					if (Mathf.Approximately(currentPosition.x, 0f) || Mathf.Approximately(currentPosition.x, FieldWidth))
						direction.InverseX();

					if (Mathf.Approximately(currentPosition.y, 0f) || Mathf.Approximately(currentPosition.y, FieldHeight))
						direction.InverseY();
				}
				
				if (currValue.HasFlag(FieldState.Gates)) // попали в ворота
				{
					StartCoroutine(Goal(currValue));
				}
                currentTurnLengthCounter++;
            }
            isLongShot = false;
            NextTurn();
		}
        else
		{            
			if (!availableDirections.Contains(direction))
				return;
            
			currentTurnLengthCounter++;
            
			var newPosition = currentPosition + direction;


			//DrawLine(currentPosition, newPosition, currentPlayer.Color);
			currentPlayer.LineDrawer.DrawLine(currentPosition, newPosition);
			DrawPoint(newPosition, currentPlayer.Color, .2f);

			if (GetFieldAtPos(newPosition).HasFlag(FieldState.Gates))
			{
				StartCoroutine(Goal(GetFieldAtPos(newPosition)));
				return;
			}

			SetFlagAtPos(newPosition, currentPlayer == Player1 ? FieldState.Player1 : FieldState.Player2);
            
			if (direction.IsDiagonal)
				SetFieldAtPos(currentPosition + direction * 0.5f, currentPlayer == Player1 ? FieldState.Player1 : FieldState.Player2);
            
			currentPosition = newPosition;


			availableDirections = GetAvailableDirections();


			if (!availableDirections.Any()) // Если некуда дальше идти...
			{
				this.isLongShot = true; // то бьем штрафной удар

				if (currentTurnLengthCounter >= TurnLength) // Если это конец хода
					NextTurn(false); // то штрафной бьет этот же игрок
                else
					NextTurn(true);
			}
            else if (currentTurnLengthCounter >= TurnLength) // Переход хода к следующему игроку
				NextTurn();
			// Если нет, то продолжаем ходить

		}
	}

	IEnumerator Goal(FieldState currValue)
	{
		if (currValue.HasFlag(FieldState.Player1))
		{
			player2Score++;
		}		
		if (currValue.HasFlag(FieldState.Player2))
		{
			player1Score++;
		}

		if (currentPlayer != null)
			currentPlayer.EndTurn();

		yield return new WaitForSeconds(3f);

		NewGame();
	}

	IEnumerable<Direction> GetAvailableDirections()
	{
		foreach (Direction direction in Direction.All)
		{
			var newPosValue = GetFieldAtPos(currentPosition + direction);

			if (newPosValue == FieldState.Empty)
			{
				if (direction.IsDiagonal && GetFieldAtPos(currentPosition + direction * 0.5f) != FieldState.Empty)
					continue;

				yield return direction;
			}

			if (newPosValue.HasFlag(FieldState.Gates))
			{
				yield return direction;
			}
		}
	}

	void OnDrawGizmos()
	{
		DebugDrawPoints();
	}

    #region Helper methods
	public FieldState GetFieldAtPos(Vector2 pos)
	{
		if (Mathf.Approximately(pos.x, Mathf.Floor(pos.x)) && Mathf.Approximately(pos.y, Mathf.Floor(pos.y)))
		{
			return gameField [Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y)];
		}

		return fieldsCenters [Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y)];
	}
    
	void SetFieldAtPos(Vector2 pos, FieldState value)
	{
		if (Mathf.Approximately(pos.x, Mathf.Floor(pos.x)) && Mathf.Approximately(pos.y, Mathf.Floor(pos.y)))
		{
			gameField [Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y)] = value;
			return;
		}
        
		fieldsCenters [Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y)] = value;
	}
	
	void SetFlagAtPos(Vector2 pos, FieldState flag)
	{
		if (Mathf.Approximately(pos.x, Mathf.Floor(pos.x)) && Mathf.Approximately(pos.y, Mathf.Floor(pos.y)))
		{
			gameField [Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y)] |= flag;
			return;
		}
		
		fieldsCenters [Mathf.FloorToInt(pos.x), Mathf.FloorToInt(pos.y)] |= flag;
	}

	void DrawLine(Vector2 start, Vector2 end, Color color)
	{
		var line = Instantiate(LinePrefab) as LineRenderer;
		line.SetColors(color, color);
		line.SetVertexCount(2);
		line.SetColors(color, color);
		line.SetPosition(0, start);
		line.SetPosition(1, end);
		line.transform.parent = this.transform;
	}
	void DrawPoint(Vector3 pos, Color color, float scale = .3f)
	{
		var smallPoint = Instantiate(SmallPointPrefab) as SpriteRenderer;
		smallPoint.transform.position = pos;
		smallPoint.transform.localScale *= scale;
		smallPoint.color = color;
		smallPoint.transform.parent = this.transform;
	}

	void DebugDrawPoints()
	{
		if (gameField == null)
			return;

		for (int i = 0; i <= FieldWidth; i++)
		{
			for (int j = 0; j <= FieldHeight; j++)
			{
				if (gameField [i, j] == FieldState.Empty)
					DrawGizmoPoint(new Vector2(i, j), Color.white, 0.07f);
				if (gameField [i, j].HasFlag(FieldState.Border))
					DrawGizmoPoint(new Vector2(i, j), Color.red);
				if (gameField [i, j].HasFlag(FieldState.Gates))
					DrawGizmoPoint(new Vector2(i, j), Color.yellow, 0.2f);
				if (gameField [i, j].HasFlag(FieldState.Player1))
					DrawGizmoPoint(new Vector2(i, j), Color.cyan, 0.05f);
				if (gameField [i, j].HasFlag(FieldState.Player2))
					DrawGizmoPoint(new Vector2(i, j), Color.magenta, 0.05f);
			}
		}

		for (int i = 0; i < FieldWidth; i++)
		{
			for (int j = 0; j < FieldHeight; j++)
			{
				if (fieldsCenters [i, j] == FieldState.Empty)
					DrawGizmoPoint(new Vector2(i, j) + new Vector2(.5f, .5f), Color.white, 0.07f);
				if (fieldsCenters [i, j].HasFlag(FieldState.Border))
					DrawGizmoPoint(new Vector2(i, j) + new Vector2(.5f, .5f), Color.red);
				if (fieldsCenters [i, j].HasFlag(FieldState.Gates))
					DrawGizmoPoint(new Vector2(i, j) + new Vector2(.5f, .5f), Color.yellow, 0.2f);
				if (fieldsCenters [i, j].HasFlag(FieldState.Player1))
					DrawGizmoPoint(new Vector2(i, j) + new Vector2(.5f, .5f), Color.cyan, 0.05f);
				if (fieldsCenters [i, j].HasFlag(FieldState.Player2))
					DrawGizmoPoint(new Vector2(i, j) + new Vector2(.5f, .5f), Color.magenta, 0.05f);
			}
		}
	}

	void DrawGizmoPoint(Vector3 pos, Color color, float radius = 0.1f)
	{
		Gizmos.color = color;
		Gizmos.DrawSphere(pos + this.transform.position, radius);
	}
    #endregion
}
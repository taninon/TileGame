using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMoveController : SingletonMonoBehaviour<UnitMoveController>
{
	private PlayerUnit unitItendedToMove;
	private TileCell[] allTiles;
	private TileCellSugoroku[] allSugorokuTiles;

	public void SetMoveUnit(PlayerUnit target)
	{
		unitItendedToMove = target;
	}

	public void MoveToUnit(TileCell moveToTile)
	{
		unitItendedToMove.SetMoveTile(moveToTile);

		foreach (var tile in allTiles)
		{
			tile.MoveEnd();
		}
	}

	public void MoveToUnit(TileCellSugoroku moveToTile)
	{
		unitItendedToMove.SetMoveTile(moveToTile);

		foreach (var tile in allSugorokuTiles)
		{
			tile.MoveEnd();
		}
	}

	private void Awake()
	{
		allTiles = GetComponentsInChildren<TileCell>();
		allSugorokuTiles = GetComponentsInChildren<TileCellSugoroku>();
	}

}

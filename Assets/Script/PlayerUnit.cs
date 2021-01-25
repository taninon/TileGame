using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUnit : MonoBehaviour
{
	[SerializeField] private TileCell nowPositionTile;
	[SerializeField] private TileCellSugoroku nowPositionTileSugoroku;

	[SerializeField] private int stepValue;


	public void SetMoveTile(TileCell tile)
	{
		nowPositionTile = tile;
		this.transform.position = nowPositionTile.transform.position;
	}

	public void SetMoveTile(TileCellSugoroku tile)
	{
		nowPositionTileSugoroku = tile;
		this.transform.position = nowPositionTileSugoroku.transform.position;
	}

	public void OnMouseDown()
	{
		if (nowPositionTile)
		{
			nowPositionTile.SetStep(stepValue);
		}

		if (nowPositionTileSugoroku)
		{
			nowPositionTileSugoroku.SetStep(stepValue);
		}

		UnitMoveController.Instance.SetMoveUnit(this);
	}


}

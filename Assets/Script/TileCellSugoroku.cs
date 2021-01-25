using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class TileRelation
{
	public TileCellSugoroku toTile;
	public bool Movable;

	public TileRelation(TileCellSugoroku tile)
	{
		toTile = tile;
	}

}

public class TileCellSugoroku : MonoBehaviour
{
	[SerializeField] private List<TileRelation> relations = new List<TileRelation>();

	[SerializeField] internal List<TileCellSugoroku> borderOnTiles;

	public int needStep = 1;
	public int nowStep = -1;

	[SerializeField] internal TextMesh number;
	[SerializeField] internal GameObject enableEffect;


	private bool IsMovable(TileCellSugoroku toTile)
	{
		foreach (var relation in relations)
		{
			if (relation.toTile == toTile)
			{
				return relation.Movable;
			}
		}

		return false;
	}



	public void SetStep(int count)
	{
		if (count < 0 || nowStep > count)
		{
			return;
		}

		if (number)
		{
			number.text = count.ToString();
		}

		nowStep = count;

		if (nowStep == 0)
		{
			enableEffect.SetActive(true);
		}

		foreach (var tile in borderOnTiles)
		{
			if (IsMovable(tile))
			{
				var nextStepCount = count - tile.needStep;
				tile.SetStep(nextStepCount);


			}
		}
	}

	public void SetRelation(List<TileCellSugoroku> cells)
	{
		borderOnTiles = new List<TileCellSugoroku>();
		relations = new List<TileRelation>();

		foreach (var cell in cells)
		{
			if (cell == this)
			{
				continue;
			}

			float distance = Vector3.Distance(cell.transform.position, this.transform.position);
			if (distance < 1.1f)
			{
				borderOnTiles.Add(cell);
				relations.Add(new TileRelation(cell));
			}
		}

		enableEffect = transform.Find("Plane").gameObject;
		number = GetComponentInChildren<TextMesh>();
	}

	public void OnMouseDown()
	{
		if (!enableEffect.activeInHierarchy)
		{
			return;
		}


		UnitMoveController.Instance.MoveToUnit(this);
	}

	public void MoveEnd()
	{
		enableEffect.SetActive(false);
		nowStep = -1;
	}
}


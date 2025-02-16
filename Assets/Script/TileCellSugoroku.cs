using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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

	public int needStep = 1;
	public int nowStep = -1;

	[SerializeField] internal TextMeshPro number;
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
			enableEffect?.SetActive(true);
		}

		foreach (var relation in relations)
		{
			if (IsMovable(relation.toTile))
			{
				var nextStepCount = count - relation.toTile.needStep;
				relation.toTile.SetStep(nextStepCount);
			}
		}
	}

	public void SetRelation(List<TileCellSugoroku> cells)
	{
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
				relations.Add(new TileRelation(cell));
			}
		}

		enableEffect = transform.Find("Plane").gameObject;
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


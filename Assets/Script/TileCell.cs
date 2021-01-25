using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileCell : MonoBehaviour
{
	[SerializeField]
	internal List<TileCell> borderOnTiles;

	public int needStep = 1;
	public int nowStep = -1;

	[SerializeField] internal TextMesh number;
	[SerializeField] internal GameObject enableEffect;

	// Start is called before the first frame update
	private void Start()
	{

	}

	public virtual void SetStep(int count)
	{
		if (count < 0 || nowStep > count)
		{
			return;
		}

		number.text = count.ToString();
		nowStep = count;

		enableEffect.SetActive(true);

		foreach (var tile in borderOnTiles)
		{
			var nextStepCount = count - tile.needStep;
			tile.SetStep(nextStepCount);
		}
	}


	public void SetRelation(List<TileCell> cells)
	{
		borderOnTiles.Clear();
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
			}
		}
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

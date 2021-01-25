using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PuzzleSetting : MonoBehaviour
{
	[ContextMenu("AddTileCell")]
	public void AddTileCell()
	{
		var mastGameObject = GetComponentsInChildren<Transform>().Select(t => t.gameObject);

		foreach (Transform child in transform)
		{
			if (!child.GetComponent<PuzzleCell>())
			{
				child.gameObject.AddComponent<PuzzleCell>();
			}
		}
	}

	[ContextMenu("SetRelationCell")]
	public void TileCellSetRelations()
	{
		var cells = GetComponentsInChildren<PuzzleCell>();
		foreach (var cell in cells)
		{
			cell.SetRelation(cells.ToList());
		}
	}
}

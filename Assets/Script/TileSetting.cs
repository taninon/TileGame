
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TileSetting : MonoBehaviour
{

	[ContextMenu("AddTileCell")]
	public void AddTileCell()
	{
		var mastGameObject = GetComponentsInChildren<Transform>().Select(t => t.gameObject);

		foreach (Transform child in transform)
		{
			if (!child.GetComponent<TileCell>())
			{
				child.gameObject.AddComponent<TileCell>();
			}
		}
	}

	[ContextMenu("remove")]
	public void RemoveTileCell()
	{
		foreach (Transform child in transform)
		{
			if (child.GetComponent<TileCell>())
			{
				DestroyImmediate(child.gameObject.GetComponent<TileCell>());
			}
		}
	}

	[ContextMenu("SetRelationCell")]
	public void TileCellSetRelations()
	{
		var cells = GetComponentsInChildren<TileCell>();
		foreach (var cell in cells)
		{
			cell.SetRelation(cells.ToList());
		}
	}


	// Start is called before the first frame update
	private void Start()
	{
		TileCellSetRelations();
	}

	// Update is called once per frame
	private void Update()
	{

	}
}

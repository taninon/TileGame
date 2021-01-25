using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleCell : MonoBehaviour
{

	[SerializeField] internal List<PuzzleCell> borderOnCells;
	[SerializeField] internal GameObject enableEffect;

	public bool Already => enableEffect.activeInHierarchy;
	public bool ConnectSearch;

	public void OnConnect()
	{
		PuzzleController.Instance.AddConnect(this);

		foreach (var cell in borderOnCells)
		{
			if (cell.Already && !PuzzleController.Instance.IsConnected(cell))
			{
				cell.OnConnect();
			}
		}
		ConnectSearch = false;
	}

	public void SetRelation(List<PuzzleCell> cells)
	{
		borderOnCells = new List<PuzzleCell>();

		foreach (var cell in cells)
		{
			if (cell == this)
			{
				continue;
			}

			float distance = Vector3.Distance(cell.transform.position, this.transform.position);
			if (distance < 1.1f)
			{
				borderOnCells.Add(cell);
			}
		}

		enableEffect = transform.Find("Plane").gameObject;
	}


	public void OnMouseDown()
	{
		if (enableEffect.activeInHierarchy || PuzzleController.Instance.guardClick)
		{
			return;
		}

		enableEffect.SetActive(true);
		StartCoroutine(PuzzleController.Instance.StartSearchConnect());
		OnConnect();
	}



	// Update is called once per frame
	private void Update()
	{

	}
}

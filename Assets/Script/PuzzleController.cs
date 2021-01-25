using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleController : SingletonMonoBehaviour<PuzzleController>
{
	public List<PuzzleCell> connectCells = new List<PuzzleCell>();
	private bool searching;

	public GameObject deleteText;

	public bool guardClick;

	public void AddConnect(PuzzleCell cell)
	{
		connectCells.Add(cell);
		cell.ConnectSearch = true;
	}

	public bool IsConnected(PuzzleCell cell)
	{
		return connectCells.Contains(cell);
	}

	public IEnumerator StartSearchConnect()
	{
		guardClick = true;

		connectCells.Clear();
		yield return null;
		if (connectCells.Count >= 4)
		{
			yield return StartCoroutine(Delete());
		}

		guardClick = false;
	}

	private IEnumerator Delete()
	{
		deleteText.SetActive(true);

		yield return new WaitForSeconds(0.1f);
		foreach (var cell in connectCells)
		{
			yield return new WaitForSeconds(0.1f);
			cell.enableEffect.SetActive(false);
		}


		yield return new WaitForSeconds(1f);
		connectCells.Clear();
		deleteText.SetActive(false);
	}
}

using UnityEngine;
using System.Collections;

public class DefaultGameOverStrategy : IGameOver {

	public GameObject resultsPanel;
	public GameObject confirmButton;

	public override void PerformGameOver()
	{
		NGUITools.SetActive (resultsPanel, true);
		NGUITools.SetActive (confirmButton, true);
	}
}

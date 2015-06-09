using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Ball;

public class PlayerManager : MonoBehaviour
{
	private PhotonView view;
	private Ball ball;

	private Vector3 move;
	private bool jump;
	
	public void Start ()
	{
		view = GetComponent<PhotonView> ();
		ball = GetComponent <Ball> ();
	}

	public void Update ()
	{
		ball.Move (move, jump);
	}

	[RPC]
	public void updateClientMove (Vector3 move, bool jump)
	{
		this.move = move;
		this.jump = jump;
	}
}

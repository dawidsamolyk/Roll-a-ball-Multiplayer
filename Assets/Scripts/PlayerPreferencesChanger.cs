using UnityEngine;
using System.Collections;
using UnityStandardAssets.Vehicles.Ball;

public class PlayerPreferencesChanger : MonoBehaviour {
	private Ball ball;

	void Start() 
	{
		ball = GetComponent<Ball> ();
	}
	
	void OnCollisionEnter (Collision col)
	{
		GameObject gameObject = col.gameObject;
		bool destroy = false;

		switch (gameObject.tag) 
		{
		case "SpeedUp": 
			ball.SpeedUp(); 
			destroy = true; 
			break;
		case "SpeedDown": 
			ball.SlowDown(); 
			destroy = true; 
			break;
		case "JumpHigher": 
			ball.JumpHigher(); 
			destroy = true; 
			break;
		case "JumpLower": 
			ball.JumpLower(); 
			destroy = true; 
			break;
		default:
			break;   
		}

		if (destroy) 
		{
			gameObject.SetActive(false);
			PhotonNetwork.Destroy(gameObject);
		}
	}
}

using UnityEngine;
using System.Collections;

public class CameraTarget : MonoBehaviour {

	public Transform player1;
	public Transform player2;

	public float distanceBetweenPlayers;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
	{
		transform.position = player1.position / 2 + player2.position / 2 + new Vector3(0,0.75f,0);
		distanceBetweenPlayers = Vector3.Distance(player1.position, player2.position);
	}
}

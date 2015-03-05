using UnityEngine;
using System.Collections;

public class CameraSwapPosition : MonoBehaviour {

	public Transform linkedCameraSpot;
	public int id;
	private Transform camera1;
	private Transform camera2;

	// Use this for initialization
	void Start () 
	{
		try
		{
			camera1 = GameObject.Find ("Main Camera").transform;
		}
		catch
		{
			Debug.LogError("Couldn't find an object named 'Main Camera'");
		}

		try
		{
			camera2 = GameObject.Find ("Camera Player 2").transform;
		}
		catch
		{
			Debug.LogError("Couldn't find an object named 'Camera Player 2'");
		}
	}
	
	// Update is called once per frame
	void OnTriggerEnter (Collider collider) 
	{
		if (collider.name == "ThirdPersonController1")
		{
			camera1.position = linkedCameraSpot.position;
		}
		if (collider.name == "ThirdPersonController2")
		{
			camera2.position = linkedCameraSpot.position;
		}

		collider.GetComponent<ZoneTracker>().zone = id;
	}
}

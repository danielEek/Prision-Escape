using UnityEngine;
using System.Collections;

public class SplitScreenController : MonoBehaviour {

	public CameraController mainCamera;
	public CameraController cameraPlayer2;

	public bool splitScreen;
	public float splitScreenFactor;
	public float splitScreenSpeed = 3;
	public float splitScreenFieldOfView = 25;

	// Use this for initialization
	void Start () 
	{
	
	}

	public void SetSplitScreen (bool setSplitScreen)
	{
		splitScreen = setSplitScreen;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (splitScreen)
		{
			splitScreenFactor = Mathf.Lerp (splitScreenFactor, 0.5f, splitScreenSpeed * Time.deltaTime);
		}
		else
		{
			splitScreenFactor = Mathf.Lerp (splitScreenFactor, 0f, splitScreenSpeed * Time.deltaTime);
		}
		if (splitScreenFactor < 0.01f)
		{
			splitScreenFactor = 0;
		}
		else if (splitScreenFactor > 0.49f)
		{
			splitScreenFactor = 0.5f;
		}

		mainCamera.splitScreenFactor = splitScreenFactor;
		mainCamera.splitScreenFieldOfView = splitScreenFieldOfView;
		mainCamera.splitScreen = splitScreen;
		cameraPlayer2.splitScreenFactor = splitScreenFactor;
		cameraPlayer2.splitScreenFieldOfView = splitScreenFieldOfView;

	}
}

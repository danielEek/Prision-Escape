using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {

	public Transform cameraTarget;
	public Transform nonSplitScreenTarget;
	public Transform splitScreenTarget;
	public Transform angleCalculator;

	public Transform player1;
	public Transform player2;

	public float splitScreenFactor = 0.5f;
	public bool splitScreenIsRight;

	public float requiredAngleToSeePlayer;
	public float verticalFieldOfView;
	public float distanceToCameraTarget;

	public float angleToPlayer;

	public float minimumFieldOfView = 10;
	public float maximumFieldOfView = 45;

	public float extraFieldOfView = 10;

	private float distancePlayerAndCenter;

//	private CameraTarget target;
	public bool splitScreen;
	public float splitScreenFieldOfView;
	public float fieldOfViewLimitBeforeSplitScreen;
	public float splitScreenFieldOfViewThreshold = 2;

	private Camera thisCam;

	private ZoneTracker zonePlayer1;
	private ZoneTracker zonePlayer2;


	// Use this for initialization
	void Start () 
	{
//		target = nonSplitScreenTarget.GetComponent<CameraTarget>();
		thisCam = transform.GetComponent<Camera>();

		zonePlayer1 = player1.GetComponent<ZoneTracker>();
		zonePlayer2 = player2.GetComponent<ZoneTracker>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		if (splitScreenTarget != null && splitScreen)
		{
			cameraTarget = splitScreenTarget;
		}

		if (nonSplitScreenTarget != null && !splitScreen)
		{
			cameraTarget = nonSplitScreenTarget;
		}

		if (splitScreen)
		{
			if (splitScreenIsRight)
			{
				thisCam.rect = new Rect(splitScreenFactor,0,1-splitScreenFactor, 1);
			}
			else
			{
				thisCam.rect = new Rect(0,0,splitScreenFactor, 1);
			}
		}
		else
		{
			thisCam.rect = new Rect(0,0,1,1);
		}

		transform.LookAt(cameraTarget);
		angleCalculator.LookAt(nonSplitScreenTarget);
		distanceToCameraTarget = Vector3.Distance(transform.position, nonSplitScreenTarget.position);
	//	if (nonSplitScreenTarget != null)
	//	{
		Vector3 targetDir1 = player1.position - transform.position;
		float angleToPlayer1 = Vector3.Angle(targetDir1, angleCalculator.forward);
		angleToPlayer1 = Mathf.Clamp(angleToPlayer1, 0, 44);
		Vector3 targetDir2 = player2.position - transform.position;
		float angleToPlayer2 = Vector3.Angle(targetDir2, angleCalculator.forward);
		angleToPlayer2 = Mathf.Clamp (angleToPlayer2, 0, 44);
		angleToPlayer = Mathf.Max(angleToPlayer1, angleToPlayer2);
		distancePlayerAndCenter = Mathf.Tan (angleToPlayer * Mathf.Deg2Rad) * distanceToCameraTarget;

		requiredAngleToSeePlayer = Mathf.Tan(distancePlayerAndCenter / distanceToCameraTarget);
		requiredAngleToSeePlayer = Mathf.Clamp (requiredAngleToSeePlayer, -Mathf.PI, Mathf.PI);

		verticalFieldOfView = 2 * Mathf.Atan(Mathf.Tan (requiredAngleToSeePlayer) * Screen.height / Screen.width) * Mathf.Rad2Deg;

		verticalFieldOfView += 10;
//		verticalFieldOfView *= 2;
	//	}

		verticalFieldOfView = Mathf.Clamp (verticalFieldOfView, minimumFieldOfView, maximumFieldOfView);

		if (thisCam.GetComponent<SplitScreenController>())
		{
			if (verticalFieldOfView >= maximumFieldOfView || zonePlayer1.zone != zonePlayer2.zone)
			{
				thisCam.GetComponent<SplitScreenController>().SetSplitScreen(true);
			}


			if (verticalFieldOfView < maximumFieldOfView - splitScreenFieldOfViewThreshold && zonePlayer1.zone == zonePlayer2.zone)
			{
				thisCam.GetComponent<SplitScreenController>().SetSplitScreen(false);
			}


	/*		else
			{
				thisCam.GetComponent<SplitScreenController>().SetSplitScreen(false);
			}*/
		}

		if (!splitScreen)
		{
        	transform.GetComponent<Camera>().fieldOfView = verticalFieldOfView;
		}
		else
		{
			transform.GetComponent<Camera>().fieldOfView = splitScreenFieldOfView;
		}
	}
}

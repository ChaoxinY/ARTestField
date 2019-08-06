using System;
using UnityEngine;
using UnityAD;
using System.Collections.Generic;

public class BulletTrajectoryUpdater : MonoBehaviour, IEventHandler
{ 
	#region Variables
	public GameObject bulletOrigin;
	public GameObject trajectoryPrefab;
	public int totaTrajectoryPredictions;
	public float predictionIntervals;
	private BallisticTrajectoryInfo ballisticTrajectoryInfo;
	private Touch touch;
	private Quaternion originRotation;
	private List<GameObject> trajectoryDots = new List<GameObject>();
	#endregion

	#region Initialization
	private void Awake()
	{
		StaticRefrences.EventSubject.PublisherSubscribed += SubscribeEvent;
	}

	private void Start()
	{
		bulletOrigin.transform.position = StaticRefrences.slingShotOriginPoint;
		originRotation = bulletOrigin.transform.localRotation;
		for(int i = 0; i < totaTrajectoryPredictions; i++)
		{
			GameObject trajectoryDot = GameObject.Instantiate(trajectoryPrefab, bulletOrigin.transform, false);
			trajectoryDots.Add(trajectoryDot);
		}
		EnableTrajectory(false);
	}
	#endregion

	#region Functionality
	public void SubscribeEvent(object eventPublisher, PublisherSubscribedEventArgs publisherSubscribedEventArgs)
	{
		if(publisherSubscribedEventArgs.Publisher.GetType()== typeof(SlingShot.InputHandler))
		{
			SlingShot.InputHandler inputHandler = (SlingShot.InputHandler)publisherSubscribedEventArgs.Publisher;
			inputHandler.TouchDetected += OnTouchDetected;
		}
	}

	private void OnTouchDetected(object sender, UserTouchEventArgs userTouchEventArgs)
	{
		touch = userTouchEventArgs.Touch;
		//Debugger.DebugObject(this, $"Touch position{touch.position}, called MinimumPoint{StaticRefrences.MinimumVerticalPoint}");
		if(touch.position.y < StaticRefrences.MinimumScreenVerticalPoint && touch.phase != TouchPhase.Ended)
		{
			bulletOrigin.transform.localRotation = originRotation;
			UpdateSlingShotRotation();
			float lauchForce = RigidBodyToolMethods.CalculateInputLaunchForce(touch.position);
			ballisticTrajectoryInfo = new BallisticTrajectoryInfo
			{
				gravity = StaticRefrences.Gravity,
				initialVelocity = lauchForce/StaticRefrences.bulletMass/Time.fixedDeltaTime,
				launchAngle = Mathf.Abs(StaticRefrences.slingShotLaunchAngle)
			};
			//Debugger.DebugObject(this, $"LaunchForce:{lauchForce}");
			if(!trajectoryDots[0].activeInHierarchy)
			{
				EnableTrajectory(true);
			}
			UpdateTrajectory();
		}
		else if (touch.position.y < StaticRefrences.MinimumScreenVerticalPoint && touch.phase == TouchPhase.Ended)
		{
			EnableTrajectory(false);
		}
	}

	private void EnableTrajectory(bool enable)
	{
		foreach(GameObject gameObject in trajectoryDots)
		{
			gameObject.SetActive(enable); 
		}
	}

	private void UpdateTrajectory()
	{
		Vector2[] positions = RigidBodyToolMethods.CalculateBallisticTrajectory(ballisticTrajectoryInfo, totaTrajectoryPredictions, predictionIntervals);
		for(int i = 0; i < positions.Length; i++)
		{
			trajectoryDots[i].transform.localPosition =  new Vector3(0, positions[i].y, positions[i].x);
		}
	}

	private void UpdateSlingShotRotation()
	{
		Vector2 originPoint = StaticRefrences.ScreenCenterPoint;

		float adjescentLength = Math.Abs(originPoint.y - touch.position.y);
		float oppositeLength = Math.Abs(originPoint.x - touch.position.x);
		float angle = (float)Math.Atan(oppositeLength/adjescentLength) * Mathf.Rad2Deg;
		//Debugger.DebugObject(this, $"adjescentLength:{adjescentLength} oppositeLength:{oppositeLength}");
		int rotationDirection = touch.position.x > originPoint.x ? -1 : 1;
		bulletOrigin.transform.Rotate(new Vector3(0, 1, 0), angle*rotationDirection/1.8f, Space.World);
	}
	#endregion
}


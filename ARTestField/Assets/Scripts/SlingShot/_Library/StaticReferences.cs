﻿using UnityEngine;
using UnityAD;

public static class StaticReferences
{
	public static EventSubject EventSubject = new EventSubject();
	public static CoroutineToolMethods CoroutineToolMethods = new CoroutineToolMethods();
	public static SystemToolMethods SystemToolMethods = new SystemToolMethods();
	public static UnityAD.UIToolMethods UIToolMethods = new UnityAD.UIToolMethods();

	#region SlingShot
	public static float slingShotMaximumLaunchForce = 2f;
	public static float slingShotLaunchAngle = -30f;
	public static float Gravity { get; } = 9.81f;
	public static float bulletMass = 0.3f;
	public static float MinimumScreenVerticalPoint { get; } = Screen.height * 0.3f;
	public static float slingShotAngleCounterWeight = 1.8f;
	public static Vector3 slingShotOriginPoint = new Vector3(0, -0.15f, 0.7f);
	public static Vector2 ScreenCenterPoint { get; } = new Vector2(Screen.width * 0.5f, MinimumScreenVerticalPoint);
	public static int TotalTrajectoryPredictions { get; } = 15;
	public static float predictionIntervals= 0.03f;
	#endregion

	#region AI
	public static PathMap currentPathMap;
	#endregion

	#region Time
	public static int FixedTimeInMiliseconds { get { return (int)(Time.fixedDeltaTime*1000f);  } }
	#endregion
}


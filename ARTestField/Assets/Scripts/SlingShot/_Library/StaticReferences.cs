using UnityEngine;
using UnityAD;

public static class StaticRefrences
{
	public static EventSubject EventSubject = new EventSubject();
	public static CoroutineToolMethods CoroutineToolMethods = new CoroutineToolMethods();
	public static UnityAD.UIToolMethods UIToolMethods = new UnityAD.UIToolMethods();

	#region SlingShot
	public static float slingShotMaximumLaunchForce = 5f;
	public static float slingShotLaunchAngle = -20f;
	public static float Gravity { get; } = 9.81f;
	public static float bulletMass = 0.3f;
	public static float MinimumScreenVerticalPoint { get; } = Screen.height * 0.3f;
	public static float slingShotAngleCounterWeight = 1.8f;
	public static Vector3 slingShotOriginPoint = new Vector3(0, -0.1f, 0.6f);
	public static Vector2 ScreenCenterPoint { get; } = new Vector2(Screen.width * 0.5f, MinimumScreenVerticalPoint);
	#endregion


}


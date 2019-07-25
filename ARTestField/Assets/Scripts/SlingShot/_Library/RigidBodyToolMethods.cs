using System;
using UnityEngine;

public class RigidBodyToolMethods
{
	public static Vector2[] CalculateBallisticTrajectory(BallisticTrajectoryInfo ballisticTrajectoryInfo, int totalPredictions, float predictionInterval)
	{
		Vector2[] positions = new Vector2[totalPredictions];
		float initialVelocity = ballisticTrajectoryInfo.initialVelocity;
		float time = 0;
		for(int i = 0; i < totalPredictions; i++)
		{
			time += predictionInterval;
			float horizontalDisplacement = initialVelocity * time*(float)Math.Cos(ballisticTrajectoryInfo.launchAngle*Mathf.Deg2Rad);
			float verticalDisplacement = initialVelocity * time *(float)Math.Sin(ballisticTrajectoryInfo.launchAngle*Mathf.Deg2Rad) - 0.5f*ballisticTrajectoryInfo.gravity*time*time;
			Vector2 predictedPosition = new Vector2(horizontalDisplacement, verticalDisplacement);
			positions[i] = predictedPosition;
		}
		return positions;
	}
		
	public static Vector3 CalculateSlingShotLaunchForce(Vector3 forwardDirection, Vector3 touchPosition)
	{
		Vector3 force = Vector3.zero;
		float launchForce = CalculateInputLaunchForce(touchPosition);
		force = forwardDirection * launchForce;
		return force;
	}

	public static float CalculateInputLaunchForce(Vector3 touchPosition)
	{
		float launchForce = StaticRefrences.slingShotMaximumLaunchForce * Mathf.Clamp(Vector2.Distance(StaticRefrences.ScreenCenterPoint, touchPosition) / (Screen.width * 0.3f), 0, 1f);
		return launchForce;
	}
}

[Serializable]
public struct BallisticTrajectoryInfo
{
	public float gravity;
	public float initialVelocity;
	public float launchAngle;
}


using System;
using UnityEngine;
using UnityAD;

public static class FeedBackToolMethods
{
	#region Variables
	#endregion

	#region Functionality

	//H
	public static void VibrateAndroidDevice(long vibrationLength, int vibrationStrength=1)
	{
		//var unityPlayerClass = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
		//var context = unityPlayerClass.GetStatic<AndroidJavaObject>("currentActivity");

		//AndroidJavaObject vibrator = context.Call<AndroidJavaObject>("getSystemService", "vibrator");
		//AndroidJavaClass vibrationEffectClass = new AndroidJavaClass("android.os.VibrationEffect");
		//object[] parameters = { vibrationLength, vibrationStrength };
		//AndroidJavaObject vibrationEffect = vibrationEffectClass.CallStatic<AndroidJavaObject>("createOneShot", parameters);
		//vibrator.Call("vibrate", vibrationEffect);
	}

	public static void SpawnOnHitEffect(GameObject prefabToSpawn, Collision collision)
	{
		Vector3 hitSpeed = collision.relativeVelocity;
		GameObject spawnedOnHitEffect = GameObject.Instantiate(prefabToSpawn, collision.contacts[0].point , Quaternion.LookRotation(hitSpeed.normalized));
	}
	#endregion
}


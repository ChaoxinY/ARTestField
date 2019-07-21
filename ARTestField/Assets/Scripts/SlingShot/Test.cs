﻿using System;
using UnityEngine;

public class Test : MonoBehaviour
{
	#region Variables
	public Vector2 offset;
	private Vector2 origin = new Vector2(1,1);
	public Quaternion originRotation;
	public BallisticTrajectoryInfo ballisticTrajectoryInfo;
	public GameObject dummy;
	public GameObject parent;
	public float distance;
	#endregion

	#region Initialization
	private void Start()
	{
		originRotation = transform.rotation;
	}
	#endregion

	#region Functionality
	private void Update()
	{
		if(parent!= null)
		{
			transform.position = parent.transform.position + parent.transform.forward * distance + parent.transform.up *distance;
		}
	}

	public void CalculateAngle()
	{
		float angle = Vector2.Angle( origin, origin+offset );
		Debugger.DebugObject(this, $"Offset{origin+offset} Angle: {angle.ToString()}");
	}

	public void Rotate()
	{
		transform.Rotate(Vector3.up, 45);
	}

	public void ResetRotation()
	{
		transform.rotation = originRotation;
	}

	public void SpawnTrajectory()
	{
		Vector2[] positions = RigidBodyToolMethods.CalculateBallisticTrajectory(ballisticTrajectoryInfo, 100, 0.1f);
		for(int i = 0; i < positions.Length; i++)
		{
			GameObject.Instantiate(dummy, new Vector3(0, positions[i].y, positions[i].x), Quaternion.identity);
		}
	}
	#endregion
}


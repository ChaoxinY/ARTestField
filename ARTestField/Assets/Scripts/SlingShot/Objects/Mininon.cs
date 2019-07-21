﻿using System;
using UnityEngine;
using UnityAD;

public class Mininon : MonoBehaviour
{
	#region Variables
	public MinionPreset minionPreset;
	private MinionModule minionModule;
	#endregion

	#region Initialization
	private void Awake()
	{
		minionModule = MinionFactory.CreateMinionModule(minionPreset,gameObject);
	}
	#endregion

	#region Functionality
	private void OnCollisionEnter(Collision collision)
	{
		minionModule.ReactToCollision(gameObject, collision);
	}
	#endregion
}


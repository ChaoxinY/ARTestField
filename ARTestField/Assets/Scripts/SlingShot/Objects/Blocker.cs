using System;
using UnityEngine;

public class Blocker : MonoBehaviour
{
	#region Variables
	public PathingInformation pathingInformation;
	private PathingModule pathingModule;
	#endregion

	#region Initialization
	private void Start()
	{
		pathingModule = PathingModuleFactory.CreatePathingModule(gameObject, pathingInformation);
	}
	#endregion

	#region Functionality
	private void FixedUpdate()
	{
		pathingModule.FixedUpdateComponent();
	}
	#endregion
}


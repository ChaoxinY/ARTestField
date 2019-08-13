using System;
using UnityEngine;

public class SelfDestructor : MonoBehaviour
{
	#region Variables
	[SerializeField]
	private float lifeTime;
	#endregion

	#region Initialization
	private void Start()
	{
		Destroy(gameObject, lifeTime);
	}
	#endregion

	#region Functionality
	#endregion
}


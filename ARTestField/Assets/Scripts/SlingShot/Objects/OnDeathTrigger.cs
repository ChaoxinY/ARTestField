using System;
using UnityEngine;
using UnityEngine.Events;

public class OnDeathTrigger :MonoBehaviour
{
	#region Variables
	public UnityEvent onDeathEvent;
	#endregion

	#region Initialization
	#endregion

	#region Functionality

	private void OnDestroy()
	{
		onDeathEvent.Invoke();
	}
	#endregion
}




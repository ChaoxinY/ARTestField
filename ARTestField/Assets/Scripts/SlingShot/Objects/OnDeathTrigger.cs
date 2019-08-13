using System;
using UnityEngine;
using UnityEngine.Events;

public class OnDeathTrigger :MonoBehaviour
{
	#region Variables
	[SerializeField]
	private GameObject eventSender;
	[SerializeField]
	private UnityEvent onDeathEvent;
	#endregion

	#region Initialization
	#endregion

	#region Functionality

	private void OnDestroy()
	{
		if(eventSender.activeInHierarchy)
		{
			onDeathEvent.Invoke();
		}
	}
	#endregion
}




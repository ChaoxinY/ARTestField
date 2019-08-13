using System;
using UnityEngine;
using UnityEngine.UI;
using UnityAD;

public class BulletSpawner : MonoBehaviour, IEventHandler
{
	#region Variables
	[SerializeField]
    private GameObject bulletPrefab;
    private Touch touch;
	#endregion

	#region Initialization
	private void Awake()
	{
		StaticReferences.EventSubject.PublisherSubscribed += SubscribeEvent;
	}
    #endregion

    #region Functionality
	public void SubscribeEvent(object eventPublisher, PublisherSubscribedEventArgs publisherSubscribedEventArgs)
	{
		if(publisherSubscribedEventArgs.Publisher.GetType()== typeof(SlingShot.InputHandler))
		{
			SlingShot.InputHandler inputHandler = (SlingShot.InputHandler)publisherSubscribedEventArgs.Publisher;
			inputHandler.TouchDetected += OnTouchDetected;
		}
	}

	public void UnSubScribeEvent()
	{
		StaticReferences.EventSubject.PublisherSubscribed -= SubscribeEvent;
		foreach(IEventPublisher eventPublisher in StaticReferences.EventSubject.EventPublishers)
		{
			if(eventPublisher.GetType()== typeof(SlingShot.InputHandler))
			{
				SlingShot.InputHandler inputHandler = (SlingShot.InputHandler)eventPublisher;
				inputHandler.TouchDetected -= OnTouchDetected;
			}
		}
	}

	private void OnDestroy()
	{
		UnSubScribeEvent();
	}

	private void OnTouchDetected(object sender, UserTouchEventArgs userTouchEventArgs)
    {
        touch = userTouchEventArgs.Touch;

        if (touch.position.y <StaticReferences.MinimumScreenVerticalPoint)
        {
			if(touch.phase == TouchPhase.Ended)
			{
				//Debugger.DebugObject(this, $"{RigidBodyToolMethods.CalculateSlingShotLaunchForce(transform.forward, touch.position)}");
				transform.Rotate(new Vector3(1, 0, 0),StaticReferences.slingShotLaunchAngle);
				FireSlingShot(RigidBodyToolMethods.CalculateSlingShotLaunchForce(transform.forward,touch.position));
			}         
        }
    }
    private void FireSlingShot(Vector3 force)
    {	
		GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponentInChildren<Rigidbody>().AddForce(force, ForceMode.Impulse);
	}
	#endregion
}


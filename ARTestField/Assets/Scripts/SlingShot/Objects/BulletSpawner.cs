﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UnityAD;

public class BulletSpawner : MonoBehaviour, IEventHandler
{
	#region Variables
    public GameObject bulletPrefab;
    public Text debugText;
    private Touch touch;
	#endregion

	#region Initialization
	private void Awake()
	{
		StaticRefrences.EventSubject.PublisherSubscribed += SubscribeEvent;
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

	private void OnTouchDetected(object sender, UserTouchEventArgs userTouchEventArgs)
    {
        touch = userTouchEventArgs.Touch;

        if (touch.position.y <StaticRefrences.MinimumScreenVerticalPoint)
        {
			if(touch.phase == TouchPhase.Ended)
			{
				//Debugger.DebugObject(this, $"{RigidBodyToolMethods.CalculateSlingShotLaunchForce(transform.forward, touch.position)}");
				transform.Rotate(new Vector3(1, 0, 0),StaticRefrences.slingShotLaunchAngle);
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


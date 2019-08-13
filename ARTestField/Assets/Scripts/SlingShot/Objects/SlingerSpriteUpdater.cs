using System;
using UnityEngine;
using UnityEngine.UI;
using UnityAD;

public class SlingerSpriteUpdater : MonoBehaviour, IEventHandler
{
    #region Variables
	[SerializeField]
    private RectTransform slingerSpriteTransform;
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

	private void OnTouchDetected(object eventPublisher, UserTouchEventArgs userTouchEventArgs)
    {      
        touch = userTouchEventArgs.Touch;
		//Debugger.DebugObject(this, $"Touch position{touch.position}, called MinimumPoint{StaticRefrences.MinimumVerticalPoint}");
		if(touch.position.y < StaticReferences.MinimumScreenVerticalPoint)
		{
				UpdateSlingerSpritePosition();
        }
    }

    private void UpdateSlingerSpritePosition()
    {
		if(touch.phase == TouchPhase.Ended)
		{
			slingerSpriteTransform.position = StaticReferences.ScreenCenterPoint;
		}
		else
		{
			slingerSpriteTransform.position = touch.position;
		}
	}
    #endregion
}


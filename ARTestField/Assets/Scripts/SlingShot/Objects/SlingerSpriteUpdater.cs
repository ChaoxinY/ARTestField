using System;
using UnityEngine;
using UnityEngine.UI;
using UnityAD;

public class SlingerSpriteUpdater : MonoBehaviour, IEventHandler
{
    #region Variables
    public RectTransform slingerSpriteTransform;
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

	private void OnTouchDetected(object eventPublisher, UserTouchEventArgs userTouchEventArgs)
    {      
        touch = userTouchEventArgs.Touch;
		//Debugger.DebugObject(this, $"Touch position{touch.position}, called MinimumPoint{StaticRefrences.MinimumVerticalPoint}");
		if(touch.position.y < StaticRefrences.MinimumScreenVerticalPoint)
		{
				UpdateSlingerSpritePosition();
        }
    }

    private void UpdateSlingerSpritePosition()
    {
		if(touch.phase == TouchPhase.Ended)
		{
			slingerSpriteTransform.position = StaticRefrences.ScreenCenterPoint;
		}
		else
		{
			slingerSpriteTransform.position = touch.position;
		}
	}
    #endregion
}


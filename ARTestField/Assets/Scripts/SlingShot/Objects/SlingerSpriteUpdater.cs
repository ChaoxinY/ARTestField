using System;
using UnityEngine;
using UnityEngine.UI;
using UnityAD;

public class SlingerSpriteUpdater : MonoBehaviour
{
    #region Variables
    public RectTransform slingerSpriteTransform;
    private Touch touch;
    #endregion

    #region Initialization
    private void Start()
    {
		//Debugger.DebugObject(this, $"Transform Position: {slingerSpriteTransform.anchoredPosition} Screen measurement:{Screen.width} ,{Screen.height} SlingerOriginPoint: {StaticRefrences.SlingerOriginPoint}");
        SubscribeEvent();
    }
    #endregion

    #region Functionality
    private void SubscribeEvent()
    {
        foreach (IEventPublisher eventPublisher in StaticRefrences.EventSubject.EventPublishers)
        {         
            if (eventPublisher.GetType() == typeof(SlingShot.InputHandler))
            {
                SlingShot.InputHandler inputHandler = (SlingShot.InputHandler)eventPublisher;
                inputHandler.TouchDetected += OnTouchDetected;
            }
        }
    }

    private void OnTouchDetected(object eventPublisher, UserTouchEventArgs userTouchEventArgs)
    {      
        touch = userTouchEventArgs.Touch;
		//Debugger.DebugObject(this, $"Touch position{touch.position}, called MinimumPoint{StaticRefrences.MinimumVerticalPoint}");
		if(touch.position.y < StaticRefrences.MinimumVerticalPoint)
		{
				UpdateSlingerSpritePosition();
        }
    }

    private void UpdateSlingerSpritePosition()
    {
		if(touch.phase == TouchPhase.Ended)
		{
			slingerSpriteTransform.position = StaticRefrences.SlingerOriginPoint;
		}
		else
		{
			slingerSpriteTransform.position = touch.position;
		}
	}
    #endregion
}


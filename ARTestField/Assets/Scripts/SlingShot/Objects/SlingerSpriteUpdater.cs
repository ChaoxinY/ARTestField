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
        Debug.Log($"ObjectName{ToString()} called");
        touch = userTouchEventArgs.Touch;
        if (touch.position.y > StaticRefrences.MinimumVerticalPoint)
        {
            UpdateSlingerSpritePosition();
        }
    }

    private void UpdateSlingerSpritePosition()
    {
        if (touch.phase == TouchPhase.Ended)
        {
            slingerSpriteTransform.position = StaticRefrences.SlingerOriginPoint;
        }
        slingerSpriteTransform.position = touch.position;
    }
    #endregion
}


using System;
using UnityAD;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayGuide : MonoBehaviour, IEventHandler
{
	#region Variables
	public GameObject scoreDisplay;
	public GameObject guideTextGameObject;
	private Text guideText;
	#endregion

	#region Initialization
	private void Awake()
	{
		StaticReferences.EventSubject.PublisherSubscribed += SubscribeEvent;
		scoreDisplay.SetActive(false);
	}
	#endregion

	#region Functionality
	public void SubscribeEvent(object eventPublisher, PublisherSubscribedEventArgs publisherSubscribedEventArgs)
	{
		if(publisherSubscribedEventArgs.Publisher.GetType() == typeof(SlingShot.InputHandler))
		{
			SlingShot.InputHandler inputHandler = (SlingShot.InputHandler)publisherSubscribedEventArgs.Publisher;
			inputHandler.PlaneSelected += OnPlaneSelected;
		}

		else if(publisherSubscribedEventArgs.Publisher.GetType() == typeof(StageSpawner))
		{
			StageSpawner stageSpawner = (StageSpawner)publisherSubscribedEventArgs.Publisher;
			stageSpawner.StageDeleted += OnStageDeleted;
		}
	}

	public void UnSubScribeEvent()
	{
		StaticReferences.EventSubject.PublisherSubscribed -= SubscribeEvent;
		foreach(IEventPublisher eventPublisher in StaticReferences.EventSubject.EventPublishers)
		{
			if(eventPublisher.GetType() == typeof(SlingShot.InputHandler))
			{
				SlingShot.InputHandler inputHandler = (SlingShot.InputHandler)eventPublisher;
				inputHandler.PlaneSelected -= OnPlaneSelected;
			}

			else if(eventPublisher.GetType() == typeof(StageSpawner))
			{
				StageSpawner stageSpawner = (StageSpawner)eventPublisher;
				stageSpawner.StageDeleted -= OnStageDeleted;
			}
		}
	}

	private void OnDestroy()
	{
		UnSubScribeEvent();
	}

	private void OnStageDeleted(object sender, EventArgs e)
	{
		guideTextGameObject.SetActive(true);
		scoreDisplay.SetActive(false);
	}

	private void OnPlaneSelected(object eventPublisher, PlaneSelectedEventArgs planeSelectedEventArgs)
	{
		guideTextGameObject.SetActive(false);
		scoreDisplay.SetActive(true);
	}
	#endregion
}


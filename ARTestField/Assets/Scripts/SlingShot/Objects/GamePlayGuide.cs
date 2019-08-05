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
		StaticRefrences.EventSubject.PublisherSubscribed += SubscribeEvent;
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


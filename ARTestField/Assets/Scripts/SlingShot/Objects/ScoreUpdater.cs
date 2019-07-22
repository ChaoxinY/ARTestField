﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UnityAD;

public class ScoreUpdater : MonoBehaviour, IEventHandler
{
	#region Variables
	public Text scoreText;
	private int score;
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
		if(publisherSubscribedEventArgs.Publisher.GetType()== typeof(MinionModule))
		{
			MinionModule minionModule = (MinionModule)publisherSubscribedEventArgs.Publisher;
			minionModule.MinionHit += UpdateScoreText;
		}
	}

	private void UpdateScoreText(object eventPublisher, MinionOnHitEventArgs minionOnHitEventArgs)
	{
		score += minionOnHitEventArgs.MinionValue;
		scoreText.text = score.ToString();
	}
	#endregion
}

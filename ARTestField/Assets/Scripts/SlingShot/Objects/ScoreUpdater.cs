using System;
using UnityEngine;
using UnityEngine.UI;
using UnityAD;

public class ScoreUpdater : MonoBehaviour, IEventHandler
{
	#region Variables
	[SerializeField]
	private Text scoreText;
	private int score;
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
		if(publisherSubscribedEventArgs.Publisher.GetType()== typeof(MinionModule))
		{
			MinionModule minionModule = (MinionModule)publisherSubscribedEventArgs.Publisher;
			minionModule.MinionHit += UpdateScoreText;
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
			if(eventPublisher.GetType()== typeof(MinionModule))
			{
				MinionModule minionModule = (MinionModule)eventPublisher;
				minionModule.MinionHit -= UpdateScoreText;
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
		score = 0;
		scoreText.text = score.ToString();
	}

	private void UpdateScoreText(object eventPublisher, MinionOnHitEventArgs minionOnHitEventArgs)
	{
		score += minionOnHitEventArgs.MinionValue;
		scoreText.text = score.ToString();
	}

	#endregion
}


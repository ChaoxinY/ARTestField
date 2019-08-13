using System;
using UnityEngine;
using UnityEngine.UI;
using UnityAD;
using GoogleARCore;
using System.Collections.Generic;

public class StageSpawner : MonoBehaviour, IEventHandler, IEventPublisher
{
	#region Variables
	public event EventHandler StageDeleted;

	[SerializeField]
	private List<GameObject> availableStages;
	[SerializeField]
	private InputField stageInputfield;
	private bool stageSpawned;
	private TrackableHit trackableHit;
	private GameObject stage;
	private GameObject stagePrefabToSpawn;
	#endregion

	#region Initialization
	private void Awake()
	{
		StaticReferences.EventSubject.PublisherSubscribed += SubscribeEvent;
		stagePrefabToSpawn = availableStages[0];
	}
	private void Start()
	{
		StaticReferences.EventSubject.Subscribe(this);
	}
	#endregion

	#region Functionality
	public void ClearStage()
	{
		Destroy(stage);
		stageSpawned = false;
		StageDeleted?.Invoke(this, new EventArgs());
	}

	public void UnSubscribeFromSubject()
	{
		StaticReferences.EventSubject.UnSubscribe(this);
	}

	public void SetCurrentStage()
	{
		stagePrefabToSpawn = availableStages[Mathf.Clamp(UtilityLibrary.GetIntValueFromInputField(stageInputfield), 0, availableStages.Count-1)];
		Debug.Log($"currentStage {stagePrefabToSpawn}");
	}

	public void SubscribeEvent(object eventPublisher, PublisherSubscribedEventArgs publisherSubscribedEventArgs)
	{
		if(publisherSubscribedEventArgs.Publisher.GetType() == typeof(SlingShot.InputHandler))
		{
			SlingShot.InputHandler inputHandler = (SlingShot.InputHandler)publisherSubscribedEventArgs.Publisher;
			inputHandler.PlaneSelected += OnPlaneSelected;
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
				inputHandler.PlaneSelected -= OnPlaneSelected;
			}
		}
	}

	private void OnDestroy()
	{
		UnSubScribeEvent();
	}

	private void OnPlaneSelected(object eventPublisher, PlaneSelectedEventArgs planeSelectedEventArgs)
	{
		trackableHit = planeSelectedEventArgs.TrackableHit;
		if(!stageSpawned)
		{
			SpawnStage();
			stageSpawned = true;
		}
	}

	private void SpawnStage()
	{
		stage = Instantiate(stagePrefabToSpawn, trackableHit.Pose.position + Vector3.up *0.8f, Quaternion.identity);
		var anchor = trackableHit.Trackable.CreateAnchor(trackableHit.Pose);
		//Prevent static gameobject to slip away.
		stage.transform.parent = anchor.transform;
	}
	#endregion
}


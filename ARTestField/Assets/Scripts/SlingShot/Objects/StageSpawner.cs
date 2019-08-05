﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UnityAD;
using GoogleARCore;
using System.Collections.Generic;

public class StageSpawner : MonoBehaviour, IEventHandler, IEventPublisher
{
	#region Variables
	public List<GameObject> availableStages;
  
    public Text debugText;
	public InputField stageInputfield;
	public event EventHandler StageDeleted;
	private bool stageSpawned;
    private GameObject stage;
	private TrackableHit trackableHit;
	private GameObject stagePrefabToSpawn;
	#endregion

	#region Initialization
	private void Awake()
	{
		StaticRefrences.EventSubject.PublisherSubscribed += SubscribeEvent;
		stagePrefabToSpawn = availableStages[0];
		Debug.Log(StaticRefrences.EventSubject);
	}
	private void Start()
	{
		StaticRefrences.EventSubject.Subscribe(this);
	}
	#endregion

	#region Functionality
	public void ClearStage()
    {
        Destroy(stage);
        stageSpawned = false;
		StageDeleted(this, new EventArgs());
	}

	public void UnSubscribeFromSubject()
	{
		StaticRefrences.EventSubject.UnSubscribe(this);
	}

	public void SetCurrentStage()
	{
		stagePrefabToSpawn = availableStages[Mathf.Clamp(UtilityLibrary.GetIntValueFromInputField(stageInputfield),0,availableStages.Count-1)];
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

	private void OnPlaneSelected(object eventPublisher, PlaneSelectedEventArgs planeSelectedEventArgs)
    {
        trackableHit = planeSelectedEventArgs.TrackableHit;
        if (!stageSpawned)
        {
            SpawnStage();
            stageSpawned = true;
        }    
    }

    private void SpawnStage()
    {
        stage = Instantiate(stagePrefabToSpawn, trackableHit.Pose.position + Vector3.up *0.8f, Quaternion.identity);
        var anchor = trackableHit.Trackable.CreateAnchor(trackableHit.Pose);
        // Make Andy model a child of the anchor.
        //Prevent static gameobject to slip away.
        stage.transform.parent = anchor.transform;
    }

	private void OnDestroy()
	{
		UnSubscribeFromSubject();
	}
	#endregion
}


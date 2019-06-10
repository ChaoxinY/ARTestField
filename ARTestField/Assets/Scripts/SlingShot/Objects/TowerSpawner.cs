﻿using System;
using UnityEngine;
using UnityEngine.UI;
using UnityAD;
using GoogleARCore;

public class TowerSpawner : MonoBehaviour
{
    #region Variables
    private TrackableHit trackableHit;
    public GameObject towerPrefabToSpawn;
    public Text debugText;
    private bool towerSpawned;
    private GameObject tower;
    #endregion

    #region Initialization
    private void Start()
    {
        SubscribeEvent();
    }
    #endregion

    #region Functionality
    public void ClearTower()
    {
        Destroy(tower);
        towerSpawned = false;
    }

    private void SubscribeEvent()
    {
        foreach (IEventPublisher eventPublisher in StaticRefrences.EventSubject.EventPublishers)
        {
            if (eventPublisher.GetType() == typeof(SlingShot.InputHandler))
            {
                SlingShot.InputHandler inputHandler = (SlingShot.InputHandler)eventPublisher;
                inputHandler.PlaneSelected += OnPlaneSelected;
            }
        }
    }

    private void OnPlaneSelected(object eventPublisher ,PlaneSelectedEventArgs planeSelectedEventArgs)
    {
        trackableHit = planeSelectedEventArgs.TrackableHit;
        if (!towerSpawned)
        {
            SpawnTower();
            towerSpawned = true;
        }    
    }

    private void SpawnTower()
    {
        tower = Instantiate(towerPrefabToSpawn, trackableHit.Pose.position, Quaternion.identity);
        var anchor = trackableHit.Trackable.CreateAnchor(trackableHit.Pose);
        debugText.text = $"Tower SpawnPosition: {tower.transform.position}";
        // Make Andy model a child of the anchor.
        //Prevent static gameobject to slip away.
        tower.transform.parent = anchor.transform;
    }
    #endregion
}


using System;
using UnityEngine;
using UnityAD;
using GoogleARCore;

public class TowerSpawner : MonoBehaviour
{
    #region Variables
    private DetectedPlane detectedPlane;
    public GameObject towerPrefabToSpawn;
    #endregion

    #region Initialization
    private void Start()
    {
        SubscribeEvent();
        Debug.Log("Debug_MonitorTest_Called");
    }
    #endregion

    #region Functionality
    private void OnPlaneSelected(object eventPublisher ,PlaneSelectedEventArgs planeSelectedEventArgs)
    {
        detectedPlane = planeSelectedEventArgs.DetectedPlane;
        SpawnTower();
    }

    private void SpawnTower()
    {
        Instantiate(towerPrefabToSpawn, detectedPlane.CenterPose.position, Quaternion.identity);
    }

    private void SubscribeEvent()
    {
        Debug.Log($"CS 35:Count : {StaticRefrences.EventSubject.EventPublishers.Count}");
        foreach (IEventPublisher eventPublisher in StaticRefrences.EventSubject.EventPublishers)
        {
            Debug.Log($"CS 37:Count : {StaticRefrences.EventSubject.EventPublishers.Count}");
            if (eventPublisher.GetType() == typeof(SlingShot.SceneController))
            {
                SlingShot.SceneController sceneController = (SlingShot.SceneController)eventPublisher;
                sceneController.PlaneSelected += OnPlaneSelected;
                Debug.Log("Debug_Subscribed_Called");   
            }
        }
    }
    #endregion
}


﻿using GoogleARCore;
using UnityEngine;
using UnityAD;

namespace SlingShot
{
    public class SceneController : MonoBehaviour, IEventPublisher
    {
        //Event publisher
        public event System.EventHandler<PlaneSelectedEventArgs> PlaneSelected;
    
        void Awake()
        {
           Debug.Log( this is IEventPublisher eventPublisher);
                QuitOnConnectionErrors();
            StaticRefrences.EventSubject.Subscribe(this as IEventPublisher);
        }

        void Update()
        {
            // The session status must be Tracking in order to access the Frame.
            if (Session.Status != SessionStatus.Tracking)
            {
                int lostTrackingSleepTimeout = 15;
                Screen.sleepTimeout = lostTrackingSleepTimeout;
                return;
            }
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            ProcessTouches();
        }

        void QuitOnConnectionErrors()
        {
            //Is the permission to use the camera granted?
            if (Session.Status == SessionStatus.ErrorPermissionNotGranted)
            {
                StartCoroutine(CodelabUtils.ToastAndExit(
                    "Camera permission is needed to run this application.", 5));
            }
            else if (Session.Status.IsError())
            {
                // This covers a variety of errors.  See reference for details
                // https://developers.google.com/ar/reference/unity/namespace/GoogleARCore
                StartCoroutine(CodelabUtils.ToastAndExit(
                    "ARCore encountered a problem connecting. Please restart the app.", 5));
            }
        }

        /// <summary>
        /// To process the touches, we get a single touch and raycast it using the ARCore session to check if the user tapped on a plane.
        /// If so, we'll use that one to display the rest of the objects.
        /// </summary>
        void ProcessTouches()
        {
            Touch touch;
            if (Input.touchCount != 1 ||
                (touch = Input.GetTouch(0)).phase != TouchPhase.Began)  
            {
                return;
            }

            TrackableHit hit;
            TrackableHitFlags raycastFilter =
                TrackableHitFlags.PlaneWithinBounds |
                TrackableHitFlags.PlaneWithinPolygon;

            if (Frame.Raycast(touch.position.x, touch.position.y, raycastFilter, out hit))
            {
                PlaneSelected(this, new PlaneSelectedEventArgs(hit, hit.Trackable as DetectedPlane));
            }
        }

        public void UnSubscribeFromSubject()
        {
            StaticRefrences.EventSubject.UnSubscribe(this);
        }
    }
}
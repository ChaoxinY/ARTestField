using System;
using UnityEngine;
using UnityEngine.UI;
using UnityAD;

public class BulletSpawner : MonoBehaviour
{
    #region Variables
    public GameObject bulletPrefab;
    public Camera firstpersonCamera;
    public Text debugText;
    public float maximumLaunchForce;
    private Touch touch;
    private float lastFiredAngle;
    #endregion

    #region Initialization
    private void Start()
    {
        SubscribeEvent();
    }
    #endregion

    #region Functionality
    private void Update()
    {
        debugText.text = $"CameraPosition:{firstpersonCamera.transform.position }SpawnPosition{transform.position} FiredAngle {lastFiredAngle}";
    }
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

    private void OnTouchDetected(object sender, UserTouchEventArgs userTouchEventArgs)
    {
        Debug.Log($"ObjectName{ToString()} called");
        touch = userTouchEventArgs.Touch;
        if (touch.position.y > StaticRefrences.MinimumVerticalPoint && touch.phase == TouchPhase.Ended)
        {
            FireSlingShot(CalculateSlingShotForce());
        }
    }

    private Vector3 CalculateSlingShotForce()
    {
        Vector3 force = Vector3.zero;
        //offset from the origin to the touch
        //max drag distance is screen.width*0.3
        float maxDragDistance = Screen.width * 0.3f;
        Vector2 originPoint = StaticRefrences.SlingerOriginPoint;
        Vector2 downwardDirection = new Vector2(originPoint.x, originPoint.y + maxDragDistance);
        Vector2 touchDirection = touch.position - originPoint;
        float angle = Vector2.Angle(downwardDirection, touchDirection);
        transform.Rotate(transform.right, angle);
        lastFiredAngle = angle;
        float launchForce = maximumLaunchForce * Mathf.Clamp(Vector2.Distance(originPoint, touch.position) / (Screen.width * 0.3f), 0, 1f);     
        return force;
    }

    //Add vector parameter
    public void FireSlingShot(Vector3 force)
    {
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.GetComponentInChildren<Rigidbody>().AddForce(force, ForceMode.Impulse);
    }
    #endregion
}

